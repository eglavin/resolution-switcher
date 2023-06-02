using ResolutionSwitcher;
using static ResolutionSwitcher.Models;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
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
            throw new Exception("Device already detached");
        }

        if (selectedDevice.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.PrimaryDevice))
        {
            // TODO: Set primary monitor to the next available monitor
            throw new Exception("Device is the primary monitor");
        }

        var currentDeviceDisplayMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
        logger.AddToHistory(currentDeviceDisplayMode);

        currentDeviceDisplayMode.dmPelsWidth = 0;
        currentDeviceDisplayMode.dmPelsHeight = 0;
        currentDeviceDisplayMode.dmFields = FieldUseFlags.Position | 
                                            FieldUseFlags.PelsWidth |
                                            FieldUseFlags.PelsHeight;

        logger.AddToHistory(currentDeviceDisplayMode);

        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatus.Successful)
        {
            throw new Exception("Test failed");
        }

        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
