using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Args;
using TelegramBotBase.Controls.Hybrid;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    public class ButtonGridPagingForm : AutoCleanForm
    {

        ButtonGrid m_Buttons = null;

        public ButtonGridPagingForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;

            this.Init += ButtonGridForm_Init;
        }

        private async Task ButtonGridForm_Init(object sender, InitEventArgs e)
        {
            m_Buttons = new ButtonGrid();

            m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.ReplyKeyboard;

            m_Buttons.EnablePaging = true;
            m_Buttons.EnableSearch = true;

            m_Buttons.HeadLayoutButtonRow = new List<ButtonBase>() { new ButtonBase("Back", "back") };

            var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            ButtonForm bf = new ButtonForm();

            foreach (var c in countries)
            {
                bf.AddButtonRow(new ButtonBase(c.EnglishName, c.EnglishName));
            }

            m_Buttons.DataSource = bf;

            m_Buttons.ButtonClicked += Bg_ButtonClicked;

            this.AddControl(m_Buttons);


        }

        private async Task Bg_ButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Button == null)
                return;

            if (e.Button.Value == "back")
            {
                var start = new Menu();
                await this.NavigateTo(start);
            }
            else
            {

                await this.Device.Send($"Anda memilih : Button Text: {e.Button.Text}  dan button Value {e.Button.Value}");
            }


        }
    }
}
