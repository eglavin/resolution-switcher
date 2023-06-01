using ResolutionSwitcher;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.DeviceCaps;
using static ResolutionSwitcher.DeviceContext;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcherCli;
class AttachMonitor
{
    Logger logger;
    public AttachMonitor(Logger logger)
    {
        this.logger = logger;
    }

    public void Run(DisplayDeviceDetails selectedDevice)
    {
        if (selectedDevice.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop))
        {
            throw new Exception("Device already attached");
        }

        var desktopContext = GetDesktopDC();
        var currentDeviceWidth = GetDeviceWidth(desktopContext);
        ReleaseDesktopDC(desktopContext);

        logger.LogLine($"Current device context width: {currentDeviceWidth}");

        var currentDeviceMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
        logger.AddToHistory(currentDeviceMode);

        // Modify current device mode
        currentDeviceMode.dmPositionX -= currentDeviceWidth;
        currentDeviceMode.dmFields = FieldUseFlags.Position;

        logger.AddToHistory(currentDeviceMode);


        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatus.Successful)
        {
            throw new Exception("Test failed");
        }


        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
