using System;

using Blazor_JavascriptPart5.Entity;

namespace Blazor_JavascriptPart5.Services.Settings
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
