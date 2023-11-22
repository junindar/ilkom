using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Form;

namespace TelegramBot.Command
{
    public class Start : SplitterForm
    {
        public override async Task<bool> Open(MessageResult e)
        {
            var st = new Menu();
            await this.NavigateTo(st);
          
            return true;
        }

       



    }
}
