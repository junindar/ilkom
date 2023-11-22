using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Command
{
    public class Menu : AutoCleanForm
    {
       
        public Menu()
        {
           
           
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.None;
        }

        public override async Task Load(MessageResult message)
        {


            if (message.MessageText.Trim() == "")
                return;

            if (message.MessageText.ToLower() != "/start")
            {
              
                return;
            }

            await Device.HideReplyKeyboard(".....");
        }

        //public override async Task Action(MessageResult message)
        //{
        //    var call = message.GetData<CallbackData>();

        //    await message.ConfirmAction();


        //    if (call == null)
        //        return;

        //    message.Handled = true;

        //    switch (call.Value)
        //    {

        //        case "contact":

        //            //var search = new SearchPerForm();
        //            //await this.NavigateTo(search);
        //            break;
        //        case "document":

        //            //var list = new ListPerForm();
        //            //await this.NavigateTo(list);
        //            break;
             
        //        case "video":

        //            //var sub = new RegisterForm();
        //            //await this.NavigateTo(sub);
        //            break;
        //        case "media":

        //            //var sub = new RegisterForm();
        //            //await this.NavigateTo(sub);
        //            break;
        //        //default:

        //        //    message.Handled = false;

        //        //    break;
        //    }


        //}

        public override async Task Render(MessageResult message)
        {
           
            try
            {
                if (message.MessageText.ToLower() != "/start")
                {

                    return;
                }

              //  ButtonForm btn = new ButtonForm();


                //btn.AddButtonRow(new ButtonBase("Send Contact", new CallbackData("a", "contact").Serialize()));
                //btn.AddButtonRow(new ButtonBase("Send Document", new CallbackData("a", "document").Serialize()));
                //btn.AddButtonRow(new ButtonBase("Send Video", new CallbackData("a", "video").Serialize()));
                //btn.AddButtonRow(new ButtonBase("Send Media Group", new CallbackData("a", "media").Serialize()));

                await this.Device.Send("Selamat datang di Ilmu Komputer Bot. Silahkan pilih command/menu untuk mendapatkan informasi");
            }
            catch (Exception ex)
            {

                await this.Device.Send("Terjadi kesalahan sistem, silahkan hubungi admin", null);
            }


        }


    }
}
