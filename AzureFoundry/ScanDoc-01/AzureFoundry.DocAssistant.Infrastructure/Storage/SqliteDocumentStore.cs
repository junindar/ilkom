using System.Globalization;
using AzureFoundry.DocAssistant.Core.Storage;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace AzureFoundry.DocAssistant.Infrastructure.Storage;

public sealed class SqliteDocumentStore : IDocumentStore, IDisposable
{
    private const string _timestampFormat = "yyyy-MM-dd HH:mm:ss";

    private const string _schema = """
        CREATE TABLE IF NOT EXISTS documents (
            id         TEXT PRIMARY KEY,
            title      TEXT NOT NULL,
            type       INTEGER NOT NULL,
            timestamp  TEXT NOT NULL,
            status     INTEGER NOT NULL,
            text       TEXT NULL,
            media_type TEXT NULL,
            file_name  TEXT NULL
        );
        CREATE INDEX IF NOT EXISTS ix_documents_timestamp ON documents (timestamp DESC);
        """;

    private const string _documentColumns = "id, title, type, timestamp, status, text";

    private readonly string _fileFolder;
    private readonly string _connectionString;
    private readonly SemaphoreSlim _initializationLock = new(1, 1);
    private volatile bool _isInitialized;

    public SqliteDocumentStore(IOptions<StorageOptions> options)
    {
        Folder = string.IsNullOrWhiteSpace(options.Value.Folder)
            ? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DocAssistant")
            : options.Value.Folder;

        _fileFolder = Path.Combine(Folder, "files");

        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = Path.Combine(Folder, "docassistant.db"),
        }.ToString();
    }
    //detail sintaks pada project lampiran
    public string Folder { get; }

    public Task<Document> CreateAsync(
        string title,
        DocumentType type,
        byte[] file,
        string mediaType,
        CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(mediaType);

        return Protect(async () =>
        {
            var id = "doc-" + Guid.NewGuid().ToString("N")[..8];
            var fileName = id + GetExtension(mediaType);

           
            var timestamp = DateTime.Now;
            timestamp = timestamp.AddTicks(
                -(timestamp.Ticks % TimeSpan.TicksPerSecond));

            await using var connection = await OpenAsync(ct);

           
            var filePath = Path.Combine(_fileFolder, fileName);
            await File.WriteAllBytesAsync(filePath, file, ct);

            try
            {
                await using var cmd = connection.CreateCommand();
                cmd.CommandText = """
                    INSERT INTO documents
                        (id, title, type, timestamp, status, text, media_type, file_name)
                    VALUES
                        (@id, @title, @type, @timestamp, @status, NULL, @mediaType, @fileName)
                    """;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@type", (int)type);
                cmd.Parameters.AddWithValue("@timestamp",
                    timestamp.ToString(_timestampFormat, CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@status", (int)DocumentStatus.Unread);
                cmd.Parameters.AddWithValue("@mediaType", mediaType);
                cmd.Parameters.AddWithValue("@fileName", fileName);

                await cmd.ExecuteNonQueryAsync(ct);
            }
            catch
            {
                File.Delete(filePath);
                throw;
            }

            return new Document(
                id,
                title,
                type,
                timestamp,
                DocumentStatus.Unread,
                null);
        });
    }

    public Task<Document?> GetAsync(
        string id,
        CancellationToken ct = default) =>
        Protect(async () =>
        {
            await using var connection = await OpenAsync(ct);

            await using var cmd = connection.CreateCommand();
            cmd.CommandText =
                $"SELECT {_documentColumns} FROM documents WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            await using var reader = await cmd.ExecuteReaderAsync(ct);

            return await reader.ReadAsync(ct)
                ? Map(reader)
                : null;
        });

    public Task<IReadOnlyList<Document>> ListAsync(
        DocumentType? type,
        int max,
        CancellationToken ct = default) =>
        Protect(async () =>
        {
            await using var connection = await OpenAsync(ct);

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                SELECT {_documentColumns}
                FROM documents
                WHERE (@type IS NULL OR type = @type)
                ORDER BY timestamp DESC, id
                LIMIT @max
                """;

            cmd.Parameters.AddWithValue(
                "@type",
                type is null ? DBNull.Value : (int)type.Value);

            cmd.Parameters.AddWithValue(
                "@max",
                Math.Clamp(max, 1, 500));

            var results = new List<Document>();

            await using var reader = await cmd.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                results.Add(Map(reader));
            }

            return (IReadOnlyList<Document>)results;
        });

    public Task SaveTextAsync(
        string id,
        string text,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(text);

        return Protect(async () =>
        {
            await using var connection = await OpenAsync(ct);

            await using var cmd = connection.CreateCommand();
            cmd.CommandText =
                "UPDATE documents SET text = @text, status = @status WHERE id = @id";

            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@status", (int)DocumentStatus.Ready);
            cmd.Parameters.AddWithValue("@id", id);

            await cmd.ExecuteNonQueryAsync(ct);

            return true;
        });
    }

    public Task<DocumentFile?> GetFileAsync(
        string id,
        CancellationToken ct = default) =>
        Protect(async () =>
        {
            await using var connection = await OpenAsync(ct);

            await using var cmd = connection.CreateCommand();
            cmd.CommandText =
                "SELECT file_name, media_type FROM documents WHERE id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            await using var reader = await cmd.ExecuteReaderAsync(ct);

            if (!await reader.ReadAsync(ct) ||
                reader.IsDBNull(0) ||
                reader.IsDBNull(1))
            {
                return null;
            }

            var path = Path.Combine(
                _fileFolder,
                Path.GetFileName(reader.GetString(0)));

            return File.Exists(path)
                ? new DocumentFile(
                    await File.ReadAllBytesAsync(path, ct),
                    reader.GetString(1))
                : null;
        });

    public Task DeleteAsync(
        string id,
        CancellationToken ct = default) =>
        Protect(async () =>
        {
            await using var connection = await OpenAsync(ct);

            string? fileName = null;

            await using (var find = connection.CreateCommand())
            {
                find.CommandText =
                    "SELECT file_name FROM documents WHERE id = @id";

                find.Parameters.AddWithValue("@id", id);

                if (await find.ExecuteScalarAsync(ct) is string name)
                {
                    fileName = name;
                }
            }

            await using (var delete = connection.CreateCommand())
            {
                delete.CommandText =
                    "DELETE FROM documents WHERE id = @id";

                delete.Parameters.AddWithValue("@id", id);

                await delete.ExecuteNonQueryAsync(ct);
            }

            if (fileName is not null)
            {
                File.Delete(
                    Path.Combine(
                        _fileFolder,
                        Path.GetFileName(fileName)));
            }

            return true;
        });

 
    public void Dispose() => _initializationLock.Dispose();

    // ---- Internal ----

  
    private static async Task<T> Protect<T>(Func<Task<T>> work)
    {
        try
        {
            return await work();
        }
        catch (Exception ex)
            when (ex is SqliteException
                   or IOException
                   or UnauthorizedAccessException)
        {
            throw new StorageException(
                "Document storage error: " + ex.Message,
                ex);
        }
    }

    private static async Task Protect(Func<Task<bool>> work) =>
        await Protect<bool>(work);

    private async Task<SqliteConnection> OpenAsync(
        CancellationToken ct)
    {
        await InitializeAsync(ct);

        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(ct);

        return connection;
    }

    private async Task InitializeAsync(CancellationToken ct)
    {
        if (_isInitialized)
        {
            return;
        }

        await _initializationLock.WaitAsync(ct);

        try
        {
            if (_isInitialized)
            {
                return;
            }

            Directory.CreateDirectory(Folder);
            Directory.CreateDirectory(_fileFolder);

            await using var connection =
                new SqliteConnection(_connectionString);

            await connection.OpenAsync(ct);

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = _schema;

            await cmd.ExecuteNonQueryAsync(ct);

            _isInitialized = true;
        }
        finally
        {
            _initializationLock.Release();
        }
    }

    private static Document Map(SqliteDataReader r) => new(
        Id: r.GetString(0),
        Title: r.GetString(1),
        Type: (DocumentType)r.GetInt32(2),
        Time: DateTime.ParseExact(
            r.GetString(3),
            _timestampFormat,
            CultureInfo.InvariantCulture),
        Status: (DocumentStatus)r.GetInt32(4),
        Text: r.IsDBNull(5) ? null : r.GetString(5));

    private static string GetExtension(string mediaType) =>
        mediaType.ToLowerInvariant() switch
        {
            "image/png" => ".png",
            "image/webp" => ".webp",
            "image/jpeg" or "image/jpg" => ".jpg",
            _ => ".bin",
        };
}
