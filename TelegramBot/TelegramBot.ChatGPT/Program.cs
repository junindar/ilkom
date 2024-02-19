using Azure;
using Azure.AI.OpenAI;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;



string endpoint = "endPoint";
string key = "apiKey";
var botClient = new TelegramBotClient("botApiKey");
using CancellationTokenSource cts = new();
var client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
ChatCompletionsOptions options = new()
{

    DeploymentName = "NamaDeployment",
};
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateSendTextAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);
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

    options.Messages.Add(new ChatRequestUserMessage(message.Text));
    Response<ChatCompletions> response = await client.GetChatCompletionsAsync(options);
    ChatCompletions completions = response.Value;
    string fullResponse = completions.Choices[0].Message.Content;

   
    await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: fullResponse,
       
        disableNotification: true,
        replyToMessageId: update.Message.MessageId,
        
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








//while (true)
//{
//    Console.Write("Chat Prompt : ");
//    string line = Console.ReadLine()!;
//    if (line.Equals("exit", StringComparison.OrdinalIgnoreCase))
//    {
//        break;
//    }
//    options.Messages.Add(new ChatRequestUserMessage(line));
//    Console.WriteLine("Response : ");
//    Response<ChatCompletions> response = await client.GetChatCompletionsAsync(options);
//    ChatCompletions completions = response.Value;
//    string fullResponse = completions.Choices[0].Message.Content;
//    Console.WriteLine(fullResponse);

//}
