
using TelegramBot.Translator;
using TelegramBotBase.Builder;
using TelegramBotBase.Commands;




var bb = BotBaseBuilder
    .Create()
    .WithAPIKey("ApiKey")
    .DefaultMessageLoop()
     .WithStartForm<StartForm>()
    .NoProxy()
    .CustomCommands(a =>
    {
        a.Start("Selamat Datang");
    })
    .NoSerialization()
    .UseEnglish()
    .Build();



bb.BotCommand += async (s, en) =>
{
    FileStream fStream = null;
    switch (en.Command)
    {
        case "/start":
            // Detail pada lampiran project
            var start = new StartForm();
            await en.Device.ActiveForm.NavigateTo(start);

            break;


    }

};

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