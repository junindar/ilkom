using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Control
{
    public class ButtonTestForm : AutoCleanForm
    {


        public override async Task Action(MessageResult message)
        {

            var call = message.GetData<CallbackData>();

            await message.ConfirmAction();


            if (call == null)
                return;

            message.Handled = true;

            switch (call.Value)
            {
                case "button1":

                    await this.Device.Send("Anda memilih Button 1");

                    break;

                case "button2":

                    await this.Device.Send("Anda memilih Button 2");

                    break;

                case "button3":

                    await this.Device.Send("Anda memilih Button 3");

                    break;

                case "button4":

                    await this.Device.Send("Anda memilih Button 4");

                    break;

                case "back":

                    var st = new Menu();

                    await this.NavigateTo(st);

                    break;

                default:

                    message.Handled = false;

                    break;
            }


        }


        public override async Task Render(MessageResult message)
        {

            ButtonForm btn = new ButtonForm();

            btn.AddButtonRow(new ButtonBase("Button 1", new CallbackData("a", "button1").Serialize()), new ButtonBase("Button 2", new CallbackData("a", "button2").Serialize()));

            btn.AddButtonRow(new ButtonBase("Button 3", new CallbackData("a", "button3").Serialize()), new ButtonBase("Button 4", new CallbackData("a", "button4").Serialize()));

            btn.AddButtonRow(new ButtonBase("Google.com", "google", "https://www.google.com"), new ButtonBase("Telegram", "telegram", "https://telegram.org/"));

            btn.AddButtonRow(new ButtonBase("Back", new CallbackData("a", "back").Serialize()));

            await this.Device.Send("Silahkan Klik button di bawah :", btn);


        }
    }
}
