using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.ChangeDisplaySettings;
using static ResolutionSwitcherCli.Resolutions;

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

    public static List<DEVICE_MODE> GetDisplayModes(string deviceName)
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

    public static DisplayResolution GetDisplaySizes(string deviceName)
    {
        DisplayResolution displaySizes = new();
        try
        {
            var displayMode = GetDisplayModes(deviceName);
            foreach (var mode in displayMode)
            {
                if (!displaySizes.Width.Contains(mode.dmPelsWidth) && mode.dmPelsWidth >= MIN_WIDTH)
                {
                    displaySizes.Width.Add(mode.dmPelsWidth);
                }
                if (!displaySizes.Height.Contains(mode.dmPelsHeight) && mode.dmPelsHeight >= MIN_HEIGHT)
                {
                    displaySizes.Height.Add(mode.dmPelsHeight);
                }
            }

            displaySizes.Width.Sort();
            displaySizes.Height.Sort();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return displaySizes;

    }
}
