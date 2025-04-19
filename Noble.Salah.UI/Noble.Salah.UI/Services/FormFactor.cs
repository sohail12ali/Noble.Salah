using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Services
{
    public class FormFactor : IFormFactor
    {

        private readonly DevicePlatform platform = DeviceInfo.Platform;
        private readonly DeviceType deviceType = DeviceInfo.DeviceType;
        private readonly DeviceIdiom deviceIdiomType = DeviceInfo.Idiom;

        public DeviceOS GetDeviceOS()
        {
            if (platform.Equals(DevicePlatform.Android))
            {
                return DeviceOS.Android;
            }
            else if (platform.Equals(DevicePlatform.iOS))
            {
                return DeviceOS.iOS;
            }
            else if (platform.Equals(DevicePlatform.WinUI))
            {
                return DeviceOS.WinUI;
            }
            else if (platform.Equals(DevicePlatform.MacCatalyst))
            {
                return DeviceOS.MacOS;
            }
            else if (platform.Equals(DevicePlatform.Tizen))
            {
                return DeviceOS.Tizen;
            }
            else
            {
                return DeviceOS.Unknown;
            }
        }

        public string GetDeviceVersion()
        {
            return DeviceInfo.VersionString;
        }

        public string GetFormFactor()
        {
            return DeviceInfo.Idiom.ToString();
        }

        public HardwareType GetHardwareType()
        {
            if (deviceIdiomType.Equals(DeviceIdiom.Desktop))
            {
                return HardwareType.Desktop;
            }
            else if (deviceIdiomType.Equals(DeviceIdiom.Phone))
            {
                return HardwareType.Mobile;
            }
            else if (deviceIdiomType.Equals(DeviceIdiom.Tablet))
            {
                return HardwareType.Tablet;
            }
            else if (deviceIdiomType.Equals(DeviceIdiom.TV))
            {
                return HardwareType.TV;
            }
            else if (deviceIdiomType.Equals(DeviceIdiom.Watch))
            {
                return HardwareType.Watch;
            }
            else
            {
                return HardwareType.Unknown;
            }
        }

        public string GetPlatform()
        {
            return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;
        }

        public bool IsVirtualDevice()
        {
            return deviceType == DeviceType.Virtual;
        }
    }
}
