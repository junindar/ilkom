using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using TelegramBot.Control;
using TelegramBotBase.Builder;
using TelegramBotBase.Commands;


var bb = BotBaseBuilder
    .Create()
    .WithAPIKey("ApiKey")
    .DefaultMessageLoop()
    .WithStartForm<Menu>()
    .NoProxy()
    .CustomCommands(a =>
    {
        a.Start("Selamat Datang");
    })
    .NoSerialization()
    .UseEnglish()
    .Build();

//Update Bot commands to botfather
bb.UploadBotCommands().Wait();

bb.SetSetting(TelegramBotBase.Enums.ESettings.LogAllMessages, true);

bb.Message += (s, en) =>
{
    Console.WriteLine(en.DeviceId + " " + en.Message.MessageText + " " + (en.Message.RawData ?? ""));
};

bb.Start();

Console.WriteLine("Telegram Bot started...");
Console.WriteLine("Press q to quit application.");
Console.ReadLine();
bb.Stop();
