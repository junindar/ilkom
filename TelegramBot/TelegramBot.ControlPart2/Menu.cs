using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    public class Menu : AutoCleanForm
    {

        public Menu()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.None;
        }

        public override async Task Load(MessageResult message)
        {
         

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
                case "multitogglebuttons":

                    //detail sintaks pada lampiran project
                    var mtb = new MultiToggleButtons();
                    await this.NavigateTo(mtb);
                    break;
                case "buttongrid":
                    var bgf = new ButtonGridForm();
                    await this.NavigateTo(bgf);
                    break;
                case "buttongridfilter":
                    var bgp = new ButtonGridPagingForm();
                    await this.NavigateTo(bgp);
                    break;
               
              
                case "checkedbuttonlist":

                    var cbl = new CheckedButtonListForm();
                    await this.NavigateTo(cbl);
                    break;
                case "notifications":

                    var not = new NotificationForm();
                    await NavigateTo(not);

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


                btn.AddButtonRow(new ButtonBase("1. MultiToggleButtons", new CallbackData("a", "multitogglebuttons").Serialize()));
                btn.AddButtonRow(new ButtonBase("2. ButtonGrid", new CallbackData("a", "buttongrid").Serialize()));
                btn.AddButtonRow(new ButtonBase("3. ButtonGrid Paging & Filter", new CallbackData("a", "buttongridfilter").Serialize()));
                btn.AddButtonRow(new ButtonBase("4. CheckedButtonList", new CallbackData("a", "checkedbuttonlist").Serialize()));
                btn.AddButtonRow(new ButtonBase("5. Notifications", new CallbackData("a", "notifications").Serialize()));
                await this.Device.Send("Selamat datang di Ilmu Komputer Bot. Silahkan pilih  untuk mendapatkan informasi", btn);
               
            }
            catch (Exception ex)
            {

                await this.Device.Send("Terjadi kesalahan sistem, silahkan hubungi admin", null);
            }


        }

    }
}
