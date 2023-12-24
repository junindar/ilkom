using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Control
{
    public class Menu : AutoCleanForm
    {

        public Menu()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.None;
        }

        public override async Task Load(MessageResult message)
        {
            //if (message.MessageText.Trim() == "")
            //    return;

            if (message.MessageText.ToLower() != "/start")
            {

                return;
            }

            await Device.HideReplyKeyboard(".....");
        }

        public override async Task Action(MessageResult message)
        {
            var call = message.GetData<CallbackData>();

            await message.ConfirmAction();


            if (call == null)
                return;

            message.Handled = true;

            switch (call.Value)
            {
                case "button":

                    //detail sintaks pada lampiran project
                    var bf = new ButtonTestForm();
                    await this.NavigateTo(bf);
                    break;
                case "progress":
                    var pf = new ProgressTestForm();
                    await this.NavigateTo(pf);
                    break;
                case "calendar":
                    var calendar = new CalendarPickerForm();
                    await this.NavigateTo(calendar);
                    break;
                case "month":
                    var month = new MonthPickerForm();
                    await this.NavigateTo(month);
                    break;
              
                case "togglebuttons":

                    var tb = new ToggleButtons();
                    await this.NavigateTo(tb);
                    break;
                default:

                    message.Handled = false;

                    break;
            }


        }

        public override async Task Render(MessageResult message)
        {

            try
            {
                if (message.MessageText.ToLower() != "/start")
                {

                    return;
                }

                ButtonForm btn = new ButtonForm();


                btn.AddButtonRow(new ButtonBase("1. Button", new CallbackData("a", "button").Serialize()));
                btn.AddButtonRow(new ButtonBase("2. Progress Bar", new CallbackData("a", "progress").Serialize()));
                btn.AddButtonRow(new ButtonBase("3. Calendar Picker", new CallbackData("a", "calendar").Serialize()));
                btn.AddButtonRow(new ButtonBase("4. Month Picker", new CallbackData("a", "month").Serialize()));
       
                btn.AddButtonRow(new ButtonBase("5. ToggleButtons", new CallbackData("a", "togglebuttons").Serialize()));
                await this.Device.Send("Selamat datang di Ilmu Komputer Bot. Silahkan pilih  untuk mendapatkan informasi", btn);
               
            }
            catch (Exception ex)
            {

                await this.Device.Send("Terjadi kesalahan sistem, silahkan hubungi admin", null);
            }


        }

    }
}
