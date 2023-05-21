using System.Collections.Generic;
using System.Runtime.InteropServices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcher;
public class ChangeDisplaySettings
{
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
    private static extern DisplayChangeStatus ChangeDisplaySettingsEx(string lpszDeviceName,
                                                                     ref DEVICE_MODE lpDevMode,
                                                                     IntPtr hwnd,
                                                                     ChangeDisplaySettingsFlags dwflags,
                                                                     IntPtr lParam);

    public static DisplayChangeStatus TestDisplayMode(string deviceName, DEVICE_MODE deviceMode)
    {
        var status = ChangeDisplaySettingsEx(deviceName,
                                             ref deviceMode,
                                             IntPtr.Zero,
                                             ChangeDisplaySettingsFlags.Test,
                                             IntPtr.Zero);

        return status;
    }

    public static string ChangeDisplayMode(string deviceName, DEVICE_MODE deviceMode)
    {
        var status = ChangeDisplaySettingsEx(deviceName,
                                             ref deviceMode,
                                             IntPtr.Zero,
                                             ChangeDisplaySettingsFlags.UpdateRegistry,
                                             IntPtr.Zero);

        switch (status)
        {
            case DisplayChangeStatus.Successful:
                {
                    return "Success";
                }
            case DisplayChangeStatus.Restart:
                {
                    return "You Need To Reboot For The Change To Happen.\n If You Feel Any Problem After Rebooting Your Machine\nThen Try To Change Resolution In Safe Mode.";
                }
            default:
                {
                    return "Failed To Change The Resolution.";
                }
        }
    }
}
