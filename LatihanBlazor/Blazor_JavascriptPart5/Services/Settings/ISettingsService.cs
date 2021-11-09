using System;
using System.Threading.Tasks;
using Blazor_JavascriptPart5.Entity;

namespace Blazor_JavascriptPart5.Services.Settings
{
  public  interface ISettingsService
    {
        Setting Setting { get; set; }
        event EventHandler<SettingsChangedEventArgs> SettingsChanged;
        void RaiseSettingsChanged();
    }

}
