using System.Runtime.InteropServices;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcher;
public class Structs
{
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

    /// <summary>
    /// The DEVMODE structure is used for specifying characteristics of display and print devices in the Unicode (wide) character set.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-devmodea
    /// https://github.com/dotnet/pinvoke/blob/93de9b78bcd8ed84d02901b0556c348fa66257ed/src/Windows.Core/DEVMODE.cs#L14
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public FieldUseFlags dmFields;

        public int dmPositionX;
        public int dmPositionY;
        /// <summary>
        /// Represents the orientation(rotation) of the display. This member can be one of these values:
        /// DMDO_DEFAULT = 0 : Display is in the default orientation.
        /// DMDO_90 = 1 : The display is rotated 90 degrees (measured clockwise) from DMDO_DEFAULT.
        /// DMDO_180 = 2 : The display is rotated 180 degrees (measured clockwise) from DMDO_DEFAULT.
        /// DMDO_270 = 3 : The display is rotated 270 degrees (measured clockwise) from DMDO_DEFAULT.
        /// </summary>
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;

        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;
        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;

        public int dmDisplayFlags;
        public int dmDisplayFrequency;

        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;

        public int dmPanningWidth;
        public int dmPanningHeight;
    };
}

