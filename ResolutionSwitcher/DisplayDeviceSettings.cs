using System.Runtime.InteropServices;

namespace ResolutionSwitcher;
public class DisplayDeviceSettings
{
    public static int MIN_WIDTH = 800;
    public static int MIN_HEIGHT = 600;

    [StructLayout(LayoutKind.Sequential)]
    public struct Position
    {
        public int x;
        public int y;
    }

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
        public Position dmPosition;
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

    public class DeviceModeDetails
    {
        public DeviceModeDetails(int index, DEVICE_MODE deviceMode)
        {
            Index = index;
            Width = deviceMode.dmPelsWidth;
            Height = deviceMode.dmPelsHeight;
            DeviceMode = deviceMode;
        }

        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DEVICE_MODE DeviceMode;
    }


    /// <summary>
    /// The EnumDisplaySettings function retrieves information about one of the graphics modes for a display device.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaysettingsa
    /// <param name="lpszDeviceName">A pointer to a null-terminated string that specifies the display device about whose graphics mode the function will obtain information.</param>
    /// <param name="iModeNum">The type of information to be retrieved.</param>
    /// <param name="lpDevMode">A pointer to a DEVMODE structure into which the function stores information about the specified graphics mode.</param>
    /// <returns>If the function succeeds, the return value is nonzero.</returns>
    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string lpszDeviceName,
                                                   int iModeNum,
                                                   ref DEVICE_MODE lpDevMode);

    public static DeviceModeDetails GetCurrentDisplayMode(string deviceName)
    {
        DEVICE_MODE deviceMode = new();
        try
        {
            EnumDisplaySettings(deviceName, -1, ref deviceMode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return new DeviceModeDetails(-1, deviceMode);
    }

    public static List<DeviceModeDetails> GetDisplayModeDetails(string deviceName, bool filtered = true)
    {
        List<DeviceModeDetails> displayModeDetails = new();
        try
        {
            DEVICE_MODE deviceMode = new();

            for (int index = 0; EnumDisplaySettings(deviceName, index, ref deviceMode); index++)
            {
                if ((filtered &&
                    deviceMode.dmPelsWidth >= MIN_WIDTH &&
                    deviceMode.dmPelsHeight >= MIN_HEIGHT &&
                    displayModeDetails.FindIndex((d) => d.Width == deviceMode.dmPelsWidth &&
                                                        d.Height == deviceMode.dmPelsHeight) == -1) ||
                    !filtered)
                {
                    displayModeDetails.Add(new DeviceModeDetails(index, deviceMode));
                }
            }

            // Sort resolutions by width, height and index
            displayModeDetails.Sort((a, z) =>
            {
                var dif = a.Width - z.Width;
                if (dif == 0) dif = a.Height - z.Height;
                if (dif == 0) dif = a.Index - z.Index;
                return dif;
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return displayModeDetails;
    }

    public static void LogModeDetails(DeviceModeDetails details)
    {
        Console.WriteLine($"ID: {details.Index}\t  W: {details.Width}\t  H: {details.Height}");
    }
}
