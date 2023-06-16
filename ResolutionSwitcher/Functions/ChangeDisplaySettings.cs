using System.Runtime.InteropServices;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Structs;

namespace ResolutionSwitcher.Functions;
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
    private static extern DisplayChangeStatusFlag ChangeDisplaySettingsEx(string lpszDeviceName,
                                                                          ref DEVMODE lpDevMode,
                                                                          IntPtr hwnd,
                                                                          ChangeDisplaySettingsFlags dwflags,
                                                                          IntPtr lParam);

    /// <summary>
    /// Overload of ChangeDisplaySettingsEx that accepts nullable parameters, used to apply already saved changes.
    /// </summary>
    [DllImport("user32.dll")]
    private static extern DisplayChangeStatusFlag ChangeDisplaySettingsEx(string? lpszDeviceName,
                                                                          IntPtr lpDevMode,
                                                                          IntPtr hwnd,
                                                                          ChangeDisplaySettingsFlags dwflags,
                                                                          IntPtr lParam);

    public static DisplayChangeStatusFlag TestDisplaySettings(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.Test,
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatusFlag UpdateDisplaySettings(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset,
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatusFlag SetPrimaryDisplay(string deviceName, DEVMODE deviceMode)
    {
        return ChangeDisplaySettingsEx(deviceName,
                                       ref deviceMode,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.SetPrimary | ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset,
                                       IntPtr.Zero);
    }

    public static DisplayChangeStatusFlag ApplyDisplaySettings()
    {
        return ChangeDisplaySettingsEx(null,
                                       IntPtr.Zero,
                                       IntPtr.Zero,
                                       ChangeDisplaySettingsFlags.None,
                                       IntPtr.Zero);
    }

    public static string LogDisplayChangeStatus(DisplayChangeStatusFlag status)
    {
        switch (status)
        {
            case DisplayChangeStatusFlag.Successful:
                return "Success";

            case DisplayChangeStatusFlag.Restart:
                return "You'll need to reboot for these changes to apply";

            case DisplayChangeStatusFlag.Failed:
                return "Failed To Change The Resolution";

            case DisplayChangeStatusFlag.BadMode:
                return "The graphics mode is not supported";

            case DisplayChangeStatusFlag.NotUpdated:
                return "Unable to write settings to the registry";

            case DisplayChangeStatusFlag.BadFlags:
                return "An invalid set of flags was passed in";

            case DisplayChangeStatusFlag.BadParam:
                return "An invalid parameter was passed in. This can include an invalid flag or combination of flags.";

            case DisplayChangeStatusFlag.BadDualView:
                return "The settings change was unsuccessful because the system is DualView capable.";

            default:
                return "Failed To Change The Resolution.";
        }
    }
}
