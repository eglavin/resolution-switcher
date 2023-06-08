using ResolutionSwitcher;
using ResolutionSwitcher.Models;

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

    public static DisplayDeviceDetails AskUserToSelectDevice(List<DisplayDeviceDetails> displayDevices, Logger logger)
    {
        logger.Log("Enter the desired device id: ");
        var deviceIndexInput = Console.ReadLine();
        logger.AddToHistory(deviceIndexInput);

        // Ensure input is at lease a int value.
        if (!int.TryParse(deviceIndexInput, out _))
        {
            throw new Exception("Unable to parse input.");
        }

        var selectedDevice = displayDevices.Find((device) => device.Index.ToString() == deviceIndexInput);
        logger.AddToHistory(selectedDevice);

        if (selectedDevice == null)
        {
            throw new Exception("Unable to find device.");
        }

        return selectedDevice;
    }
}
