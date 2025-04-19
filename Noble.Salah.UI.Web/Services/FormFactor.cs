using Microsoft.JSInterop;
using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Web.Services;

public class FormFactor : IFormFactor
{
    private readonly IJSInProcessRuntime _jsRuntime;

    // Inject the IJSRuntime which in WASM can be cast to IJSInProcessRuntime
    public FormFactor(IJSRuntime jsRuntime)
    {
        _jsRuntime = (IJSInProcessRuntime)jsRuntime;
    }


    /// <summary>
    /// Uses the navigator.userAgent string to determine the underlying device OS.
    /// </summary>
    /// <returns>A DeviceOS value based on common substrings found in the user agent.</returns>
    public DeviceOS GetDeviceOS()
    {
        // Get the browser's user agent.
        var userAgent = _jsRuntime.Invoke<string>("eval", "navigator.userAgent");

        // Check for common OS indicators in the user agent string.
        if (userAgent.Contains("Android"))
            return DeviceOS.Android;
        else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
            return DeviceOS.iOS;
        else if (userAgent.Contains("Win"))
            return DeviceOS.WinUI;
        else if (userAgent.Contains("Mac"))
            return DeviceOS.MacOS;
        else if (userAgent.Contains("Tizen"))
            return DeviceOS.Tizen;
        else
            return DeviceOS.Unknown;
    }

    /// <summary>
    /// For WebAssembly, returns the navigator.appVersion from the browser.
    /// This string contains version details about the browser environment.
    /// </summary>
    /// <returns>The app version string or "Unknown" if nothing is retrieved.</returns>
    public string GetDeviceVersion()
    {
        var appVersion = _jsRuntime.Invoke<string>("eval", "navigator.appVersion");
        return string.IsNullOrEmpty(appVersion) ? "Unknown" : appVersion;
    }

    /// <summary>
    /// Since this is for WebAssembly, the form factor is simply "Web".
    /// </summary>
    /// <returns>A constant string "Web".</returns>
    public string GetFormFactor()
    {
        return "Web";
    }

    /// <summary>
    /// Infers hardware type based on screen width. This is a heuristic:
    /// - Mobile (phone): screen width less than 768 pixels.
    /// - Tablet: screen width between 768 and 1024 pixels.
    /// - Desktop: screen width greater than or equal to 1024 pixels.
    /// </summary>
    /// <returns>A HardwareType value based on screen width.</returns>
    public HardwareType GetHardwareType()
    {
        // Get the screen width from the browser
        int screenWidth = _jsRuntime.Invoke<int>("eval", "window.screen.width");

        if (screenWidth < 768)
            return HardwareType.Mobile;
        else if (screenWidth < 1024)
            return HardwareType.Tablet;
        else
            return HardwareType.Desktop;
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }

    /// <summary>
    /// In a browser environment, the concept of a virtual device does not apply,
    /// so we simply return false.
    /// </summary>
    /// <returns>False, indicating that this is not a virtual device.</returns>
    public bool IsVirtualDevice()
    {
        return false;
    }
}