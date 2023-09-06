using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.SendMessage;
using File = System.IO.File;


var botClient = new TelegramBotClient("");

using CancellationTokenSource cts = new();
List<Photo> lstPhotos = new List<Photo>();
List<BookCover> lstCovers=new List<BookCover>();

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

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateSendAudioAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);
lstCovers = getBookCovers();
var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateSendTextAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");




    string textohtml = @"Ini adalah pesan balasan untuk membuat <b>font Bold</b>, <strong> bold </strong>," +
                   @"<i> font Italic </i>, <u> font underline </u>" +
                   @"<a href = 'http://www.example.com/'> inline URL </a>";
    string textomarkdown2 = @"Ini adalah pesan balasan untuk membuat *font Bold* , _font Italic_ __font underline__";

    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: textohtml,
        parseMode: ParseMode.Html,
        disableNotification: true,
        replyToMessageId: update.Message.MessageId,
        replyMarkup: new InlineKeyboardMarkup(
            InlineKeyboardButton.WithUrl(
                text: "Help",
                url: "https://core.telegram.org/bots/api#sendmessage")),
        cancellationToken: cancellationToken);

    //FileStream fStream = new FileStream(@"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\Blazor.png", FileMode.Open);
    //Message sentMessage = await botClient.SendPhotoAsync(
    //    chatId: chatId,
    //    // photo: InputFile.FromUri("https://raw.githubusercontent.com/junindar/ilkom/master/TelegramBot/Images/AspNetCore.png"),
    //    photo: InputFile.FromStream(fStream),
    //    caption: "<b>ASP .NET Core MVC </b>. <i>Source</i>: <a href=\"https://play.google.com/store/books/details/Junindar_ASP_NET_Core_MVC?id=CIG9DwAAQBAJ&hl=en&gl=US\">Google Play</a>",
    //    parseMode: ParseMode.Html,
    //    cancellationToken: cancellationToken);
    //fStream.Close(); fStream.Dispose();



    //Message message1 = await botClient.SendStickerAsync(
    //    chatId: chatId,
    //    sticker: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"),
    //    cancellationToken: cancellationToken);

    //Message message2 = await botClient.SendStickerAsync(
    //    chatId: chatId,
    //    sticker: InputFile.FromFileId(message1.Sticker!.FileId),
    //    cancellationToken: cancellationToken);

    //if (sentMessage.From!=null && sentMessage.ReplyToMessage!=null && sentMessage.Entities!=null)
    //{
    //    Console.WriteLine(
    //        $"{sentMessage.From.FirstName} sent message {sentMessage.MessageId} " +
    //        $"to chat {sentMessage.Chat.Id} at {sentMessage.Date}. " +
    //        $"It is a reply to message {sentMessage.ReplyToMessage.MessageId} " +
    //        $"and has {sentMessage.Entities.Length} message entities.");
    //}

    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "You said:\n" + messageText,
    //    cancellationToken: cancellationToken);
}

async Task HandleUpdateSendPhotoAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");





    FileStream fStream = new FileStream(@"E:\Program\Ilkom\Artikel Program\ilkom\TelegramBot\Images\Blazor.png", FileMode.Open);
    Message sentMessage = await botClient.SendPhotoAsync(
        chatId: chatId,
        // photo: InputFile.FromUri("https://raw.githubusercontent.com/junindar/ilkom/master/TelegramBot/Images/AspNetCore.png"),
        photo: InputFile.FromStream(fStream),
        caption: "<b>ASP .NET Core MVC </b>. <i>Source</i>: <a href=\"https://play.google.com/store/books/details/Junindar_ASP_NET_Core_MVC?id=CIG9DwAAQBAJ&hl=en&gl=US\">Google Play</a>",
        parseMode: ParseMode.Html,
        cancellationToken: cancellationToken);
    fStream.Close(); fStream.Dispose();

}


async Task HandleUpdateSendMultiplePhotoAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    if (lstPhotos.Count == 0)
    {
        foreach (var itm in lstCovers)
        {
            itm.FileName = Path.GetFileName(itm.Path);
            FileStream fStream = new FileStream(itm.Path, FileMode.Open);
            Message sentMessage = await botClient.SendPhotoAsync(
                chatId: chatId,

                photo: InputFile.FromStream(fStream),
                caption: $"{itm.Caption} {itm.Url}",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
            fStream.Close(); fStream.Dispose();
            if (sentMessage.Photo != null)
            {
                var photo = sentMessage.Photo.First();
                lstPhotos.Add(new Photo
                {
                    Caption = itm.Caption,
                    FileName = itm.FileName,
                    FileId = photo.FileId,
                    Url = itm.Url,
                    FileUniqueId = photo.FileUniqueId
                });
            }



        }
    }
    else
    {
        foreach (var itm in lstPhotos)
        {
            await botClient.SendPhotoAsync(
                chatId: chatId,

                photo: InputFile.FromFileId(itm.FileId),
                caption: $"{itm.Caption} {itm.Url}",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
        }

    }



   



   

    //if (sentMessage.From!=null && sentMessage.ReplyToMessage!=null && sentMessage.Entities!=null)
    //{
    //    Console.WriteLine(
    //        $"{sentMessage.From.FirstName} sent message {sentMessage.MessageId} " +
    //        $"to chat {sentMessage.Chat.Id} at {sentMessage.Date}. " +
    //        $"It is a reply to message {sentMessage.ReplyToMessage.MessageId} " +
    //        $"and has {sentMessage.Entities.Length} message entities.");
    //}

    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "You said:\n" + messageText,
    //    cancellationToken: cancellationToken);
}

async Task HandleUpdateSendStickerAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



    Message message1 = await botClient.SendStickerAsync(
        chatId: chatId,
        sticker: InputFile.FromUri("https://raw.githubusercontent.com/junindar/ilkom/master/TelegramBot/Sticker/sticker.webp"),
        cancellationToken: cancellationToken);

    Message message2 = await botClient.SendStickerAsync(
        chatId: chatId,
        sticker: InputFile.FromFileId(message1.Sticker!.FileId),
        cancellationToken: cancellationToken);

    //if (sentMessage.From!=null && sentMessage.ReplyToMessage!=null && sentMessage.Entities!=null)
    //{
    //    Console.WriteLine(
    //        $"{sentMessage.From.FirstName} sent message {sentMessage.MessageId} " +
    //        $"to chat {sentMessage.Chat.Id} at {sentMessage.Date}. " +
    //        $"It is a reply to message {sentMessage.ReplyToMessage.MessageId} " +
    //        $"and has {sentMessage.Entities.Length} message entities.");
    //}

    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "You said:\n" + messageText,
    //    cancellationToken: cancellationToken);
}

async Task HandleUpdateSendAudioAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



    Message sentMessage = await botClient.SendAudioAsync(
        chatId: chatId,
        audio: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"),
        /*
        performer: "Joel Thomas Hunger",
        title: "Fun Guitar and Ukulele",
        duration: 91, // in seconds
        */
        cancellationToken: cancellationToken);
    var result = sentMessage;
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