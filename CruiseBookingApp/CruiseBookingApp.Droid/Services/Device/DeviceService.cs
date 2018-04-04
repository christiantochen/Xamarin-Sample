using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Telephony;
using CruiseBookingApp.Droid.Services.Device;
using CruiseBookingApp.Services.Device;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceService))]
namespace CruiseBookingApp.Droid.Services.Device
{
    public class DeviceService : IDeviceService
    {
        /// <inheritdoc/>
        public string Model => Build.Model;

        /// <inheritdoc/>
        public string Manufacturer => Build.Manufacturer;

        /// <inheritdoc/>
        public string DeviceName => Android.Provider.Settings.System.GetString(Application.Context.ContentResolver, "device_name") ?? Model;

        /// <inheritdoc/>
        public string DeviceSoftwareVersion
        {
            get
            {
                using (var telephonyManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService))
                    return telephonyManager.DeviceSoftwareVersion;
            }
        }

        /// <inheritdoc/>
        public string Version => Build.VERSION.Release;

        /// <inheritdoc/>
        public Version VersionNumber => new Version(Version);

        /// <inheritdoc/>
        public string AppVersion
        {
            get
            {
                using (var info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName,
                                                                                    PackageInfoFlags.MetaData))
                    return info.VersionName;
            }
        }

        /// <inheritdoc/>
        public string AppBuild
        {
            get
            {
                using (var info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName,
                                                                                    PackageInfoFlags.MetaData))
                    return info.VersionCode.ToString();
            }
        }
    }
}
