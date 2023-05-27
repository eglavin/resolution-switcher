using ResolutionSwitcher;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcherCli;
class DetachMonitor
{
    Logger logger;
    public DetachMonitor(Logger logger)
    {
        this.logger = logger;
    }

    public void Run(DisplayDeviceDetails selectedDevice)
    {
        if (!selectedDevice.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop))
        {
            logger.LogLine("Device already detached");
            return;
        }

        if (selectedDevice.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.PrimaryDevice))
        {
            logger.LogLine("Device is the primary monitor");

            // TODO: Set primary monitor to the next available monitor
            return;
        }

        var currentDeviceDisplayMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
        logger.AddToHistory(currentDeviceDisplayMode);

        // TODO: Disable the monitor

    }
}
