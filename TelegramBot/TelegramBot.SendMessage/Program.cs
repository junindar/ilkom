using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


var botClient = new TelegramBotClient("");

using CancellationTokenSource cts = new();




ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    //string texto = @"Ini adalah pesan balasan untuk membuat <b>font Bold</b>, <strong> bold </strong>," +
    //               @"<i> font Italic </i>, <u> font underline </u>" +
    //               @"<a href = 'http://www.example.com/'> inline URL </a>";


    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: texto,//"Ini adalah pesan balasan untuk membuat *font Bold* , _font Italic_ __font underline__ ",
    //    parseMode: ParseMode.Html,
    //    disableNotification: true,
    //    replyToMessageId: update.Message.MessageId,
    //    replyMarkup: new InlineKeyboardMarkup(
    //        InlineKeyboardButton.WithUrl(
    //            text: "Help",
    //            url: "https://core.telegram.org/bots/api#sendmessage")),
    //    cancellationToken: cancellationToken);
    // FileStream fStream = new FileStream(@"e:\ikan.jpg", FileMode.Open);
    Message sentMessage = await botClient.SendPhotoAsync(
        chatId: chatId,
        photo: InputFile.FromUri("https://raw.githubusercontent.com/junindar/ilkom/master/TelegramBot/Images/AspNetCore.png"),
        //photo: InputFile.FromStream(fStream, "test.jpg"),
        caption: "<b>ASP .NET Core MVC </b>. <i>Source</i>: <a href=\"https://play.google.com/store/books/details/Junindar_ASP_NET_Core_MVC?id=CIG9DwAAQBAJ&hl=en&gl=US\">Google Play</a>",
        parseMode: ParseMode.Html,
        cancellationToken: cancellationToken);
    //fStream.Close(); fStream.Dispose();
    //var arrPhto = sentMessage.Photo.ToList();

    //Message sentMessage2 = await botClient.SendPhotoAsync(
    //    chatId: chatId,
    //    //photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
    //    photo:InputFile.FromFileId(arrPhto.First().FileId),
    //    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
    //    parseMode: ParseMode.Html,
    //    cancellationToken: cancellationToken);

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