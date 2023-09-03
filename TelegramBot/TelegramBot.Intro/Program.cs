using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Intro;

var botClient = new TelegramBotClient("xxxxxx");//Token
List<UserProfile> userProfiles = new List<UserProfile>();
using CancellationTokenSource cts = new();


//var me = await botClient.GetMeAsync();
//Console.WriteLine($"Hi {me.Id} - BotName: {me.FirstName}.");


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

    var userProfile = userProfiles.FirstOrDefault(c => c.Id == chatId);
    string strText = "";
    if (userProfile == null)
    {
        strText = "Masukkan nama :";
        userProfile = new UserProfile();
        userProfile.Id = chatId;
        userProfiles.Add(userProfile);
    }
    else
    {
        if (string.IsNullOrEmpty(userProfile.Nama))
        {
            strText = $"HI {messageText}, Silahkan Masukkan Alamat anda:";
            userProfile.Nama = messageText;

        }
        else
        {
            userProfile.Alamat = messageText;
            strText =
                $"Data anda sebagai berikut telah kami simpan. Nama {userProfile.Nama} dan Alamat {userProfile.Alamat}. Untuk memulai pendaftaran ketik 'Hi'";
            userProfiles.Remove(userProfile);

        }
    }
    
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: strText,
        cancellationToken: cancellationToken);

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