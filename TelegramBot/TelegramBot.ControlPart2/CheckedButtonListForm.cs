﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotBase.Args;
using TelegramBotBase.Controls.Hybrid;
using TelegramBotBase.DataSources;
using TelegramBotBase.Form;

namespace TelegramBot.ControlPart2
{
    public class CheckedButtonListForm : AutoCleanForm
    {

        CheckedButtonList m_Buttons = null;

        public CheckedButtonListForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;

            this.Init += CheckedButtonListForm_Init;
        }

        private async Task CheckedButtonListForm_Init(object sender, InitEventArgs e)
        {
            m_Buttons = new CheckedButtonList();

            m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard;
            m_Buttons.EnablePaging = true;

            m_Buttons.HeadLayoutButtonRow = new List<ButtonBase>() { new ButtonBase("Back", "back"), new ButtonBase("Switch Keyboard", "switch") };

            m_Buttons.SubHeadLayoutButtonRow = new List<ButtonBase>() { new ButtonBase("No checked items", "$") };

            ButtonForm bf = new ButtonForm();

            for (int i = 0; i < 30; i++)
            {
                bf.AddButtonRow($"{i + 1}. Item", i.ToString());
            }

            m_Buttons.DataSource = new ButtonFormDataSource(bf);

            m_Buttons.ButtonClicked += Bg_ButtonClicked;
            m_Buttons.CheckedChanged += M_Buttons_CheckedChanged;

            this.AddControl(m_Buttons);
        }

        private async Task M_Buttons_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            m_Buttons.SubHeadLayoutButtonRow = new List<ButtonBase>() { new ButtonBase($"{m_Buttons.CheckedItems.Count} checked items", "$") };
        }

        private async Task Bg_ButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Button == null)
                return;

            switch (e.Button.Value)
            {
                case "back":

                    var start = new Menu();
                    await NavigateTo(start);
                    break;


                case "switch":
                    switch (m_Buttons.KeyboardType)
                    {
                        case TelegramBotBase.Enums.EKeyboardType.ReplyKeyboard:
                            m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard;
                            break;
                        case TelegramBotBase.Enums.EKeyboardType.InlineKeyBoard:
                            m_Buttons.KeyboardType = TelegramBotBase.Enums.EKeyboardType.ReplyKeyboard;
                            break;
                    }
                    break;

                default:
                    await Device.Send($"Button clicked with Text: {e.Button.Text} and Value {e.Button.Value}");
                    break;
            }


        }
    }
}
