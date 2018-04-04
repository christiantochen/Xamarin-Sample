using System;

namespace CruiseBookingApp.Services.Device
{
    public interface IDeviceService
    {
        /// <summary>
        /// Get the model of the device
        /// </summary>
        string Model { get; }

        /// <summary>
        /// Get the manufacturer of the device
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Get the name of the device
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// Get the software version number for device, for example, the IMEI/SV for GSM phones.
        /// </summary>
        string DeviceSoftwareVersion { get; }

        /// <summary>
        /// Gets the version of the operating system as a string
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the version number of the operating system as a <see cref="Version"/>
        /// </summary>
        Version VersionNumber { get; }

        /// <summary>
        /// Returns the current version of the app, as defined in the PList, e.g. "4.3".
        /// </summary>
        /// <value>The current version.</value>
        string AppVersion { get; }

        /// <summary>
        /// Returns the current build of the app, as defined in the PList, e.g. "4300".
        /// </summary>
        /// <value>The current build.</value>
        string AppBuild { get; }
    }
}
