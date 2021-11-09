using System;
using System.Threading.Tasks;
using Blazor_JavascriptPart4.Entity;

namespace Blazor_JavascriptPart4.Services.Settings
{
  public  interface ISettingsService
    {
        Setting Setting { get; set; }
        event EventHandler<SettingsChangedEventArgs> SettingsChanged;
        void RaiseSettingsChanged();
    }

}
