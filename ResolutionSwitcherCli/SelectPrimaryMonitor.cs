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

        var offsetX = currentDeviceDisplayMode.dmPositionX;
        var offsetY = currentDeviceDisplayMode.dmPositionY;
        currentDeviceDisplayMode.dmPositionX = 0;
        currentDeviceDisplayMode.dmPositionY = 0;


        var newPrimaryMonitorStatus = SetPrimaryDisplay(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        Console.WriteLine($"\nSetPrimaryDisplay ({selectedDevice.Name}): {LogDisplayChangeStatus(newPrimaryMonitorStatus)}");

        var updatedDevices = new List<Object>();
        foreach (var device in otherDevices)
        {
            if (device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
            {
                var deviceDisplayMode = GetCurrentDisplayMode(device.DisplayDevice.DeviceName).DeviceMode;

                deviceDisplayMode.dmPositionX -= offsetX;
                deviceDisplayMode.dmPositionY -= offsetY;

                var status = ChangeDisplayMode(device.DisplayDevice.DeviceName, deviceDisplayMode);
                Console.WriteLine($"ChangeDisplayMode ({device.Name}): {LogDisplayChangeStatus(status)}");

                updatedDevices.Add(new
                {
                    device,
                    deviceDisplayMode,
                    status
                });
            }
        }


        var applyChangesStatus = ApplyChanges();
        Console.WriteLine($"ApplyChanges: {LogDisplayChangeStatus(applyChangesStatus)}");

        logger.WriteOutput(new
        {
            selectedDevice,
            otherDevices,
            currentDeviceDisplayMode,
            offsetX,
            offsetY,
            newPrimaryMonitorStatus,
            updatedDevices,
            applyChangesStatus,
        });
    }
}
