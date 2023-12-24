using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    internal class NotificationForm : AutoCleanForm
    {
        bool sent = false;

        public NotificationForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;
        }

        public override async Task Action(MessageResult message)
        {
            if (message.Handled)
                return;

            switch (message.RawData)
            {
                case "alert":

                    await message.ConfirmAction("Ini adalah alert", true);

                    break;
                case "back":

                    var mn = new Menu();
                    await NavigateTo(mn);

                    break;
                default:

                    await message.ConfirmAction("Ini adalah feedback");

                    break;

            }

        }

        public override async Task Render(MessageResult message)
        {
            if (sent)
                return;

            var bf = new ButtonForm();
            bf.AddButtonRow("Normal feeback", "normal");
            bf.AddButtonRow("Alert Box", "alert");
            bf.AddButtonRow("Back", "back");

            await Device.Send("Choose your test", bf);

            sent = true;
        }
    }
}
