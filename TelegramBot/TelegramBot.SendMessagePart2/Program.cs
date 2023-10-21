using System.IO;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using TelegramBot.SendMessagePart2;

var botClient = new TelegramBotClient("");
List<BookCover> lstCovers = new List<BookCover>();

List<BookCover> getBookCovers()
{
    List<BookCover> bookCovers = new List<BookCover>
    {
        new()
        {
            Caption = "<b>ASP .NET Core MVC </b>. <i>Source</i>:",Path = @"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\AspNetCore.png",
            Url= "<a href=\"https://play.google.com/store/books/details/Junindar_ASP_NET_Core_MVC?id=CIG9DwAAQBAJ&hl=en&gl=US\">Google Play</a>"
        },
        new()
        {
            Caption = "<b>Microsoft Blazor: Membangun Aplikasi Web Dengan Mudah dan Cepat</b>. <i>Source</i>:",
            Path = @"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\Blazor.png",
            Url = "<a href=\"https://play.google.com/store/books/details/Junindar_Microsoft_Blazor_Membangun_Aplikasi_Web_D?id=HKZhEAAAQBAJ\">Google Play</a>"
        }
        ,
        new()
        {
            Caption = "<b>Mudah Membangun Web Aplikasi Dengan MudBlazor</b>. <i>Source</i>:",
            Path = @"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\MudBlazor.jpg",
            Url = "<a href=\"https://play.google.com/store/books/details/Junindar_Microsoft_Blazor_Mudah_Membangun_Web_Apli?id=q9usEAAAQBAJ\">Google Play</a>"
        },
        new()
        {
            Caption = "<b>Xamarin Android: Mudah Membangun Aplikasi Mobile </b>. <i>Source</i>:",
            Path = @"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\Xamarin.jpg",
            Url = "<a href=\"https://play.google.com/store/books/details/Junindar_Xamarin_Android?id=G4tFDgAAQBAJ\">Google Play</a>"
        }
    };

    return bookCovers;
}

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateSendVideoAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);
lstCovers = getBookCovers();
var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateSendVideoAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



    FileStream fStream = new FileStream(@"..\videoplayback.mp4", FileMode.Open);
    await botClient.SendVideoAsync(
        chatId: chatId,
        video: InputFile.FromStream(fStream),
        caption: "Video Reel Pancing",
       cancellationToken: cancellationToken);
    fStream.Close(); fStream.Dispose();


}

async Task HandleUpdateSendVideoNoteAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



    FileStream fStream = new FileStream(@"..\video-waves.mp4", FileMode.Open);
    FileStream fStreamThumb = new FileStream(@"..\reel.png", FileMode.Open);
    Message sentMessage = await botClient.SendVideoNoteAsync(
        chatId: chatId,
        videoNote: InputFile.FromStream(fStream),
        thumbnail: InputFile.FromStream(fStreamThumb),
        duration: 5,
        length: 200,
        cancellationToken: cancellationToken);
    fStream.Close(); fStream.Dispose();
    fStreamThumb.Close(); fStreamThumb.Dispose();

}


async Task HandleUpdateSendMediaGroupAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    var lstStream = new List<FileStream>();
    var media =   new List<InputMediaPhoto>();
    foreach (var itm in lstCovers)
    {
        itm.FileName = Path.GetFileName(itm.Path);
        FileStream fStream = new FileStream(itm.Path, FileMode.Open);
        media.Add(new InputMediaPhoto(InputFile.FromStream(fStream, itm.FileName)));
        lstStream.Add(fStream);
     

    }
    await botClient.SendMediaGroupAsync(
         chatId: chatId,
         media: media,
         cancellationToken: cancellationToken);
    foreach (var fStream in lstStream)
    {
       
        fStream.Close(); fStream.Dispose();
    }
   

   

}

async Task HandleUpdateSendDocumentAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



  
    FileStream fStream = new FileStream(@"..\reel.png", FileMode.Open);
   

    Message sentMessage = await botClient.SendDocumentAsync(
        chatId: chatId,
        document: InputFile.FromStream(fStream),
        caption: "<b>Reel Pancing</b>. <i>Source</i>: <a href=\"https://duniamancing.com\">Si Paling Mancing</a>",
        parseMode: ParseMode.Html,
        cancellationToken: cancellationToken);


    fStream.Close(); fStream.Dispose();
   

}


async Task HandleUpdateSendAnimationAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    await botClient.SendAnimationAsync(
    chatId: chatId,
      animation: InputFile.FromUri("https://raw.githubusercontent.com/junindar/ilkom/master/TelegramBot/video/giphy.gif"),
        caption: "Animation",
        cancellationToken: cancellationToken);
   

}


async Task HandleUpdateSendContactAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    await botClient.SendContactAsync(
         chatId: chatId,
         phoneNumber: "+1234567890",
         firstName: "Junindar",
         lastName: "Tasripin",
         cancellationToken: cancellationToken);

    await botClient.SendContactAsync(
         chatId: chatId,
         phoneNumber: "+1234567890",
         firstName: "Junindar",
         vCard: "BEGIN:VCARD\n" +
                "VERSION:3.0\n" +
                "N:Tasripin;Junindar\n" +
                "ORG:ARSYITECH\n" +
                "TEL;TYPE=voice,work,pref:+1234567890\n" +
                "EMAIL:junindar.tasripin@email.com\n" +
                "END:VCARD",
         cancellationToken: cancellationToken);


    await botClient.SendVenueAsync(
        chatId: chatId,
        latitude: 0.9830643988427209f,
        longitude: 104.09879776647044f,
        title: "RAKIT HAMBALI",
        address: "Piayu Laut, Kampung Tua, Batam",
        cancellationToken: cancellationToken);

    await botClient.SendLocationAsync(
        chatId: chatId,
        latitude: 0.9830643988427209f,
        longitude: 104.09879776647044f,
        cancellationToken: cancellationToken);

}


Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
