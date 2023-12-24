using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Controls.Inline;
using TelegramBotBase.Form;

namespace TelegramBot.Control
{
    public class ProgressTestForm : AutoCleanForm
    {

        public ProgressTestForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;
            this.Opened += ProgressTest_Opened;
            this.Closed += ProgressTest_Closed;
        }


        private async Task ProgressTest_Opened(object sender, EventArgs e)
        {
            await this.Device.Send("Selamat datang di ProgressTest");
        }
        private async Task ProgressTest_Closed(object sender, EventArgs e)
        {
            await this.Device.Send("Keluar dari ProgressTest");
        }
        public override async Task Action(MessageResult message)
        {
            var call = message.GetData<CallbackData>();

            await message.ConfirmAction();


            if (call == null)
                return;

            ProgressBar Bar = null;

            switch (call.Value)
            {
                case "standard":

                    Bar = new ProgressBar(0, 100, ProgressBar.EProgressStyle.standard);
                    Bar.Device = this.Device;

                    break;

                case "squares":

                    Bar = new ProgressBar(0, 100, ProgressBar.EProgressStyle.squares);
                    Bar.Device = this.Device;

                    break;

                case "circles":

                    Bar = new ProgressBar(0, 100, ProgressBar.EProgressStyle.circles);
                    Bar.Device = this.Device;

                    break;

                case "lines":

                    Bar = new ProgressBar(0, 100, ProgressBar.EProgressStyle.lines);
                    Bar.Device = this.Device;

                    break;

                case "squaredlines":

                    Bar = new ProgressBar(0, 100, ProgressBar.EProgressStyle.squaredLines);
                    Bar.Device = this.Device;

                    break;

                case "start":

                    var sf = new Menu();

                    await this.NavigateTo(sf);

                    return;

                default:

                    return;

            }


            //Render Progress bar and show some "example" progress
            await Bar.Render(message);

            this.Controls.Add(Bar);

            for (int i = 0; i <= 100; i++)
            {
                Bar.Value++;
                await Bar.Render(message);

            }


        }


        public override async Task Render(MessageResult message)
        {

            //detail sintaks di project lampiran
            ButtonForm btn = new ButtonForm();
            btn.AddButtonRow(new ButtonBase("Standard", new CallbackData("a", "standard").Serialize()), new ButtonBase("Squares", new CallbackData("a", "squares").Serialize()));

            btn.AddButtonRow(new ButtonBase("Circles", new CallbackData("a", "circles").Serialize()), new ButtonBase("Lines", new CallbackData("a", "lines").Serialize()));

            btn.AddButtonRow(new ButtonBase("Squared Line", new CallbackData("a", "squaredlines").Serialize()));

            btn.AddButtonRow(new ButtonBase("Back", new CallbackData("a", "start").Serialize()));

            await this.Device.Send("Pilih progress bar:", btn);
        }

      

    }
}
