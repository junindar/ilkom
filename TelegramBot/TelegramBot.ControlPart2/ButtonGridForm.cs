using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Args;
using TelegramBotBase.Controls.Hybrid;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    public class ButtonGridForm : AutoCleanForm
    {

        ButtonGrid m_Buttons = null;

        public ButtonGridForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;

            this.Init += ButtonGridForm_Init;
        }

        private async Task ButtonGridForm_Init(object sender, InitEventArgs e)
        {
            m_Buttons = new ButtonGrid();

            m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard;

            ButtonForm bf = new ButtonForm();

            bf.AddButtonRow(new ButtonBase("Back", "back"), new ButtonBase("Switch Keyboard", "switch"));

            bf.AddButtonRow(new ButtonBase("Button1", "b1"), new ButtonBase("Button2", "b2"));

            bf.AddButtonRow(new ButtonBase("Button3", "b3"), new ButtonBase("Button4", "b4"));

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
            else if (e.Button.Value == "switch")
            {
                switch (m_Buttons.KeyboardType)
                {
                    case TelegramBotBase.Enums.EKeyboardType.ReplyKeyboard:
                        m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard;
                        break;
                    case TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard:
                        m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.ReplyKeyboard;
                        break;
                }


            }
            else
            {

                await this.Device.Send($"Anda memilih : Button Text: {e.Button.Text}  dan button Value {e.Button.Value}");
            }


        }
    }
}
