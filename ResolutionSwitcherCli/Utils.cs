using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;

namespace ResolutionSwitcherCli;

static class Utils
{

    public static string GetDeviceDetails(DisplayDeviceDetails device, bool expanded = false)
    {
        if (expanded)
        {
            return $@"Device ID: {device.Index}
Name: {device.Name}
String: {device.DisplayDevice.DeviceString}
ID: {device.DisplayDevice.DeviceID}
Key: {device.DisplayDevice.DeviceKey}
Flags: {device.DisplayDevice.StateFlags}";
        }
        else
        {
            return $@"Device ID: {device.Index}
Name: {device.Name}
Flags: {device.DisplayDevice.StateFlags}";
        }

    }

    public static string GetModeHead()
    {
        return @$"{String.Format("|{0,6}|{1,8}|{2,9}|", "Id", "Width", "Height")}
{String.Format("|{0,6}|{1,8}|{2,9}|", "-----", "-----", "-----")}";
    }

    public static string GetModeRow(DeviceModeDetails mode)
    {
        return String.Format("|{0,6}|{1,8}|{2,9}|", mode.Index, mode.Width, mode.Height);
    }
}
