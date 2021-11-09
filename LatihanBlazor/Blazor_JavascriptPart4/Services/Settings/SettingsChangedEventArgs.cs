using System;

using Blazor_JavascriptPart4.Entity;

namespace Blazor_JavascriptPart4.Services.Settings
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public SettingsChangedEventArgs(Setting setting)
        {
            Setting = setting;
        }

        public Setting Setting { get; }
    }
}
