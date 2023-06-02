using System.Runtime.InteropServices;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcher.Models;
using static ResolutionSwitcher.Structs;

namespace ResolutionSwitcher;
public class DisplayDeviceSettings
{
    private static int MIN_WIDTH = 800;
    private static int MIN_HEIGHT = 600;

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
                                                   ref DEVMODE lpDevMode);

    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string lpszDeviceName,
                                                   EnumDisplayModeFlags iModeNum,
                                                   ref DEVMODE lpDevMode);

    public static DeviceModeDetails GetCurrentDisplayMode(string deviceName)
    {
        DEVMODE deviceMode = new();
        try
        {
            EnumDisplaySettings(deviceName, EnumDisplayModeFlags.CurrentSettings, ref deviceMode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return new DeviceModeDetails(-1, deviceMode);
    }

    public static List<DeviceModeDetails> GetDisplayModeDetails(string deviceName, bool filtered = true)
    {
        List<DeviceModeDetails> displayModeDetails = new();
        try
        {
            DEVMODE deviceMode = new();

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
            Console.WriteLine(ex.ToString());
        }

        return displayModeDetails;
    }
}
