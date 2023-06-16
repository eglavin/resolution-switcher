using System.Runtime.InteropServices;
using ResolutionSwitcher.Flags;

namespace ResolutionSwitcher.Functions;
public class DeviceCaps
{
    /// <summary>
    /// The GetDeviceCaps function retrieves device-specific information for the specified device.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
    /// <param name="hdc">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, DeviceCapabilityFlags nIndex);

    public static int GetDeviceWidth(IntPtr hdc)
    {
        return GetDeviceCaps(hdc, DeviceCapabilityFlags.HorzRes);
    }
}

