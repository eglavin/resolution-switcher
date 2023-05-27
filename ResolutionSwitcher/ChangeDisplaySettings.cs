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
                                                                      ref DEVMODE lpDevMode,
                                                                      IntPtr hwnd,
                                                                      ChangeDisplaySettingsFlags dwflags,
                                                                      IntPtr lParam);

    /// <summary>
    /// Overload of ChangeDisplaySettingsEx that accepts nullable parameters.
    /// </summary>
    [DllImport("user32.dll")]
    private static extern DisplayChangeStatus ChangeDisplaySettingsEx(string? lpszDeviceName,
                                                                      IntPtr lpDevMode,
                                                                      IntPtr hwnd,
                                                                      ChangeDisplaySettingsFlags dwflags,
                                                                      IntPtr lParam);

    public static DisplayChangeStatus TestDisplayMode(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.Test,
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatus ChangeDisplayMode(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       (ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset),
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatus SetPrimaryDisplay(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       (ChangeDisplaySettingsFlags.SetPrimary | ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset),
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatus ApplyChanges()
    {
        return ChangeDisplaySettingsEx(null,
                                       IntPtr.Zero,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.None,
                                       IntPtr.Zero);
    }

    public static string LogDisplayChangeStatus(DisplayChangeStatus status)
    {
        switch (status)
        {
            case DisplayChangeStatus.Successful:
                return "Success";

            case DisplayChangeStatus.Restart:
                return "You'll need to reboot for these changes to apply";

            case DisplayChangeStatus.Failed:
                return "Failed To Change The Resolution";

            case DisplayChangeStatus.BadMode:
                return "The graphics mode is not supported";

            case DisplayChangeStatus.NotUpdated:
                return "Unable to write settings to the registry";

            case DisplayChangeStatus.BadFlags:
                return "An invalid set of flags was passed in";

            case DisplayChangeStatus.BadParam:
                return "An invalid parameter was passed in. This can include an invalid flag or combination of flags.";

            case DisplayChangeStatus.BadDualView:
                return "The settings change was unsuccessful because the system is DualView capable.";

            default:
                return "Failed To Change The Resolution.";
        }
    }
}
