using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Args;
using TelegramBotBase.Controls.Inline;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    public class MultiToggleButtons : AutoCleanForm
    {
        public MultiToggleButtons()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;

            this.Init += ToggleButtons_Init;
        }

        private async Task ToggleButtons_Init(object sender, InitEventArgs e)
        {
            var mtb = new MultiToggleButton();

            mtb.Options = new List<ButtonBase>() { new ButtonBase("Option 1", "1"),
                new ButtonBase("Option 2", "2"), new ButtonBase("Option 3", "3") };
            mtb.SelectedOption = mtb.Options.FirstOrDefault();
            mtb.Toggled += Tb_Toggled;
            this.AddControl(mtb);
        }

        private void Tb_Toggled(object sender, EventArgs e)
        {
            var tb = sender as MultiToggleButton;
            if (tb.SelectedOption != null)
            {
                Console.WriteLine("Toggle " + tb.Id.ToString() + 
                                  " telah di-klik, dan  option " + 
                                  tb.SelectedOption.Value + " telah dipilih");
                return;
            }
        }
    }
}
