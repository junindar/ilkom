using System;
using System.Text.Json;
using System.Threading.Tasks;
using Blazor_JavascriptPart5.Entity;
using Microsoft.JSInterop;

namespace Blazor_JavascriptPart5.Services.Settings
{
    public class SettingsService: ISettingsService
    {
        public Setting Setting { get; set; } = new Setting
        {
            AppTitle = "Latihan Blazor",
            UserPictureUrl = "images/profile.png",
        

        };
       
        public event EventHandler<SettingsChangedEventArgs> SettingsChanged;
        public void RaiseSettingsChanged()
        {
            SettingsChanged?.Invoke(this, new SettingsChangedEventArgs(Setting));
        }
    }
   
}
