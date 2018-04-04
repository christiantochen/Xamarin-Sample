using System;
using Foundation;
using UIKit;
using CruiseBookingApp.iOS.Services.Device;
using CruiseBookingApp.Services.Device;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfo))]
namespace CruiseBookingApp.iOS.Services.Device
{
    public class DeviceInfo : IDeviceService
    {
        /// <inheritdoc/>
        public string Model => UIDevice.CurrentDevice.Model;

        /// <inheritdoc/>
        public string Manufacturer => "Apple";

        /// <inheritdoc/>
        public string DeviceName => UIKit.UIDevice.CurrentDevice.Name;

        /// <inheritdoc/>
        public string DeviceSoftwareVersion => UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();

        /// <inheritdoc/>
        public string Version => UIDevice.CurrentDevice.SystemVersion;

        /// <inheritdoc/>
        public Version VersionNumber => new Version(Version);

        /// <inheritdoc/>
        public string AppVersion => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        /// <inheritdoc/>
        public string AppBuild => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
    }
}
