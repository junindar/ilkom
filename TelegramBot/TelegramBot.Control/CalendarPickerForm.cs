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
    public class CalendarPickerForm : AutoCleanForm
    {

        public CalendarPicker Picker { get; set; }
        int? selectedDateMessage { get; set; }

        public CalendarPickerForm()
        {
            this.DeleteMode = TelegramBotBase.Enums.EDeleteMode.OnLeavingForm;
            this.Init += CalendarPickerForm_Init;
        }

        private async Task CalendarPickerForm_Init(object sender, InitEventArgs e)
        {
            this.Picker = new CalendarPicker();
            this.Picker.Title = "Pilih Tanggal";
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

            s = "Tanggal yang dipilih : " + this.Picker.SelectedDate.ToShortDateString() + "\r\n";
            s += "Bulan yang dipilih " + this.Picker.Culture.DateTimeFormat.MonthNames[this.Picker.VisibleMonth.Month - 1] + "\r\n";
            s += "Tahun yang dipilih " + this.Picker.VisibleMonth.Year.ToString();

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
