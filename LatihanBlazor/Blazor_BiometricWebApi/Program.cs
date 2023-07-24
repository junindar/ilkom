using Blazor_BiometricWebApi;
using Blazor_BiometricWebApi.DataAccess;
using Fido2NetLib;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowPublicAPI",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build()
    );
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//register fido2
var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton(new Fido2(new Fido2Configuration
{
    ServerDomain = appSettings.WebAuthnServerDomain,
    Origins = appSettings.WebAuthnOrigins,
    ServerName = appSettings.WebAuthnServerName,
}));

//db context config
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
});
string connectionString = builder.Configuration.GetConnectionString("TheConnectionString")!;
builder.Services.AddDbContext<TheDbContext>(options =>
{
    options.UseLoggerFactory(loggerFactory);
    options.UseSqlServer(connectionString);
});

var app = builder.Build();
app.UseCors("AllowPublicAPI");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthorization();

app.MapControllers();

app.Run();
