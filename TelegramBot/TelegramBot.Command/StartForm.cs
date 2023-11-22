using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Command
{
    public class StartForm : FormBase
    {

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

        public override async Task Render(MessageResult message)
        {
            try
            {
                if (message.MessageText.ToLower() != "/start")
                {
                    return;
                }
                await this.Device.Send("Selamat datang di Ilmu Komputer Bot. Silahkan pilih command/menu untuk mendapatkan informasi");
            }
            catch (Exception ex)
            {
                await this.Device.Send("Terjadi kesalahan sistem, silahkan hubungi admin", null);
            }
        }

    }
}
