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
    public class MonthPickerForm : AutoCleanForm
    {

        public MonthPicker Picker { get; set; }

        int? selectedDateMessage { get; set; }

        public MonthPickerForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;
            this.Init += MonthPickerForm_Init;
        }

        private async Task MonthPickerForm_Init(object sender, InitEventArgs e)
        {
            this.Picker = new MonthPicker();
            this.Picker.Title = "Pilih Bulan";
            this.AddControl(Picker);
        }


        public override async Task Action(MessageResult message)
        {

            switch (message.RawData)
            {
                case "back":

                    var s = new Menu();

                    await this.NavigateTo(s);

                    break;
            }

        }

        public override async Task Render(MessageResult message)
        {
            String s = "";

            s += "Bulan yang dipilih :  " + this.Picker.Culture.DateTimeFormat.MonthNames[this.Picker.SelectedDate.Month - 1] + "\r\n";
            s += "Tahun yang dipilih :" + this.Picker.VisibleMonth.Year.ToString();

            ButtonForm bf = new ButtonForm();
            bf.AddButtonRow(new ButtonBase("Back", "back"));

            if (selectedDateMessage != null)
            {
                await this.Device.Edit(this.selectedDateMessage.Value, s, bf);
            }
            else
            {
                var m = await this.Device.Send(s, bf);
                this.selectedDateMessage = m.MessageId;
            }



        }



    }
}
