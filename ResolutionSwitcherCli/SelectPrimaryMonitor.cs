using System.Runtime.InteropServices;
using ResolutionSwitcher;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcherCli;
class SelectPrimaryMonitor
{
    Logger logger;
    public SelectPrimaryMonitor(Logger logger)
    {
        this.logger = logger;
    }

    public void Run(DisplayDeviceDetails selectedDevice, List<DisplayDeviceDetails> otherDevices)
    {
        if (otherDevices.Count == 0)
        {
            Console.WriteLine("No other devices found, primary already set");
            return;
        }

        var currentDeviceDisplayMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName).DeviceMode;

        var offsetX = currentDeviceDisplayMode.dmPosition.x;
        var offsetY = currentDeviceDisplayMode.dmPosition.y;
        currentDeviceDisplayMode.dmPosition.x = 0;
        currentDeviceDisplayMode.dmPosition.y = 0;


        var newPrimaryMonitorStatus = SetPrimaryDisplay(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        Console.WriteLine($"\nSetPrimaryDisplay status ({selectedDevice.Name}): {newPrimaryMonitorStatus}");

        foreach (var device in otherDevices)
        {
            if (device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
            {
                var deviceDisplayMode = GetCurrentDisplayMode(device.DisplayDevice.DeviceName).DeviceMode;

                deviceDisplayMode.dmPosition.x -= offsetX;
                deviceDisplayMode.dmPosition.y -= offsetY;

                var status = ChangeDisplayMode(device.DisplayDevice.DeviceName, deviceDisplayMode);
                Console.WriteLine($"ChangeDisplayMode status ({device.Name}): {status}");
            }
        }


        var applyUpdateStatus = ApplyModes();
        Console.WriteLine($"ApplyModes status: {applyUpdateStatus}");
    }
}
