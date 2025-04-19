using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Interfaces;

public interface IFormFactor
{
    public string GetFormFactor();
    public string GetPlatform();
    public DeviceOS GetDeviceOS();
    public HardwareType GetHardwareType();
    public bool IsVirtualDevice();
    public string GetDeviceVersion();
}
