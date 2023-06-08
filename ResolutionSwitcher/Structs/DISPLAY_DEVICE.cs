using System.Runtime.InteropServices;
using ResolutionSwitcher.Flags;

namespace ResolutionSwitcher.Structs;

/// <summary>
/// The DISPLAY_DEVICE structure receives information about the display device specified by the iDevNum parameter of the EnumDisplayDevices function.
/// </summary>
/// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-display_devicea
/// https://github.com/dotnet/pinvoke/blob/93de9b78bcd8ed84d02901b0556c348fa66257ed/src/Windows.Core/DISPLAY_DEVICE.cs
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DISPLAY_DEVICE
{
    /// <summary>
    /// Size, in bytes, of the DISPLAY_DEVICE structure. This must be initialized prior to calling EnumDisplayDevices.
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    public int cb;

    /// <summary>
    /// An array of characters identifying the device name. This is either the adapter device or the monitor device.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string DeviceName;

    /// <summary>
    /// An array of characters containing the device context string. This is either a description of the display adapter or of the display monitor.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceString;

    /// <summary>
    /// Device state flags. It can be any reasonable combination of the following.
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    public DisplayDeviceFlags StateFlags;

    /// <summary>
    /// Not used.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceID;

    /// <summary>
    /// Reserved.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string DeviceKey;
}
