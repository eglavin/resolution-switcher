using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.Flags;

namespace ResolutionSwitcherCli;
class ChangeDisplaySettings
{
    /// <summary>
    /// The DEVMODE structure is used for specifying characteristics of display and print devices in the Unicode (wide) character set.
    /// </summary>
    /// https://www.pinvoke.net/default.aspx/Structures/DEVMODE.html
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-devmodea
    [StructLayout(LayoutKind.Sequential)]
    public struct DEVICE_MODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;

        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;

        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
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
        public int dmPosition;
        public int dmDisplayOrientation;

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

    /// <summary>
    /// The ChangeDisplaySettingsEx function changes the settings of the specified display device to the specified graphics mode.
    /// </summary>
    /// http://www.pinvoke.net/default.aspx/user32.ChangeDisplaySettingsEx
    /// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-changedisplaysettingsexa
    /// <param name="lpszDeviceName">A pointer to a null-terminated string that specifies the display device whose graphics mode will change.</param>
    /// <param name="lpDevMode">A pointer to a DEVMODE structure that describes the new graphics mode. If lpDevMode is NULL, all the values currently in the registry will be used for the display setting.</param>
    /// <param name="hwnd">Reserved; must be NULL.</param>
    /// <param name="dwflags">Indicates how the graphics mode should be changed.</param>
    /// <param name="lParam">If dwFlags is CDS_VIDEOPARAMETERS, lParam is a pointer to a VIDEOPARAMETERS structure. Otherwise lParam must be NULL.</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern DisplayChangeState ChangeDisplaySettingsEx(string lpszDeviceName,
                                                                     ref DEVICE_MODE lpDevMode,
                                                                     IntPtr hwnd,
                                                                     ChangeDisplaySettingsFlags dwflags,
                                                                     IntPtr lParam);
}
