﻿using System.Runtime.InteropServices;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcher;
public class DeviceCaps
{
    /// <summary>
    /// The GetDeviceCaps function retrieves device-specific information for the specified device.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
    /// <param name="hdc">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, DeviceCap nIndex);

    public static int GetDeviceWidth(IntPtr hdc)
    {
        return GetDeviceCaps(hdc, DeviceCap.HorzRes);
    }
}
