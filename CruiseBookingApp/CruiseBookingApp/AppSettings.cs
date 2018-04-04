using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using CruiseBookingApp.Models;
using CruiseBookingApp.Extensions;

namespace CruiseBookingApp
{
    public static class AppSettings
    {
        static ISettings Settings => CrossSettings.Current;

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));
            set => Settings.AddOrUpdateValue(nameof(User), value);
        }

        public static void RemoveUserData()
        {
            Settings.Remove(nameof(User));
        }

        public static bool UseFakes
        {
            get => Settings.GetValueOrDefault(nameof(UseFakes), false);
            set => Settings.AddOrUpdateValue(nameof(UseFakes), value);
        }
    }
}
