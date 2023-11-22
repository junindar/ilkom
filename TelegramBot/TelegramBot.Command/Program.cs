using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Command;
using TelegramBotBase.Builder;
using TelegramBotBase.Commands;

//var bb = BotBaseBuilder
//    .Create()
//    .QuickStart<StartForm>("")
//    .Build();

// bb.Start();

var bb = BotBaseBuilder
    .Create()
    .WithAPIKey("")
    .DefaultMessageLoop()
     .WithStartForm<StartForm>()
    .NoProxy()
    .CustomCommands(a =>
    {

        a.Start("Selamat Datang");
        a.Add("photo", "Send Photo");
        a.Add("document", "Send Document");
        a.Add("video", "Send Video");



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
        case "/photo":

            fStream = new FileStream(@"\Blazor.png", FileMode.Open);



            await en.Device.SendPhoto(InputFile.FromStream(fStream),
                caption:
                "<b>ASP .NET Core MVC </b>. <i>Source</i>: <a href=\"https://play.google.com/store/books/details/Junindar_ASP_NET_Core_MVC?id=CIG9DwAAQBAJ&hl=en&gl=US\">Google Play</a>",
                parseMode: ParseMode.Html);
            fStream.Close(); fStream.Dispose();


            break;
        case "/document":

            fStream = new FileStream(@"\reel.png", FileMode.Open);





            await en.Device.SendDocument(InputFile.FromStream(fStream), caption: "Reel Pancing");
            fStream.Close(); fStream.Dispose();



            break;

        case "/video":
            fStream = new FileStream(@"\videoplayback.mp4", FileMode.Open);

            await en.Device.SendVideo(InputFile.FromStream(fStream), caption: "<b>Video Reel Pancing</b>. <i>Source</i>: <a href=\"https://duniamancing.com\">Si Paling Mancing</a>", parseMode: ParseMode.Html);
            fStream.Close(); fStream.Dispose();

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

