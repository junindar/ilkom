using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Args;
using TelegramBotBase.Base;
using TelegramBotBase.Controls.Inline;
using TelegramBotBase.Form;

namespace TelegramBot.Control
{
    public class ToggleButtons : AutoCleanForm
    {
        public ToggleButtons()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;
            this.Init += ToggleButtons_Init;
        }

        private async Task ToggleButtons_Init(object sender, InitEventArgs e)
        {
            await Task.Delay(0);
            var tb = new ToggleButton();
            tb.Checked = true;
            tb.Toggled += Tb_Toggled;
            this.AddControl(tb);
        }

        private void Tb_Toggled(object sender, EventArgs e)
        {
            var tb = sender as ToggleButton;
            Console.WriteLine("Toggle button " + tb.Id.ToString() + " telah dipilih, Status : " +
                              (tb.Checked ? "Checked" : "Unchecked"));
        }

        public override async Task Action(MessageResult message)
        {
            var call = message.GetData<CallbackData>();

            await message.ConfirmAction();


            if (call == null)
            {
                switch (message.RawData)
                {
                    case "#c1_off":

                       await Device.Send("Anda memilih Off");
                        break;
                    case "#c1_on":

                        await Device.Send("Anda memilih On");
                        break;
                }
               
                return;
            }
               
            switch (call.Value)
            {
                case "back":

                    var s = new Menu();

                    await this.NavigateTo(s);

                    break;
            }

        }

        public override async Task Render(MessageResult message)
        {
            ButtonForm btn = new ButtonForm();
            btn.AddButtonRow(new ButtonBase("Back", new CallbackData("a", "back").Serialize()));
            await this.Device.Send("Kembali", btn);
        }
    }
}
