using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.ChangeDisplaySettings;

namespace ResolutionSwitcherCli;
class DisplaySettings
{
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

    public static List<DEVICE_MODE> GetDisplaySettings(string deviceName)
    {
        List<DEVICE_MODE> displaySettings = new();
        try
        {
            DEVICE_MODE devMode = new();

            for (int i = 0; EnumDisplaySettings(deviceName, i, ref devMode); i++)
            {
                displaySettings.Add(devMode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return displaySettings;
    }
}
