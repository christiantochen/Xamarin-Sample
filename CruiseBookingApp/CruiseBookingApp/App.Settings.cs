using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using CruiseBookingApp.Models;
using CruiseBookingApp.Extensions;

namespace CruiseBookingApp
{
    public partial class App
    {
        public static class Settings
        {
            static ISettings settings => CrossSettings.Current;

            public static User User
            {
                get => settings.GetValueOrDefault(nameof(User), default(User));
                set => settings.AddOrUpdateValue(nameof(User), value);
            }

            public static void RemoveUserData()
            {
                settings.Remove(nameof(User));
            }

            public static bool UseFakes
            {
                get => settings.GetValueOrDefault(nameof(UseFakes), false);
                set => settings.AddOrUpdateValue(nameof(UseFakes), value);
            }
        }
    }
}
