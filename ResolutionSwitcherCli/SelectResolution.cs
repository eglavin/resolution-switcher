using ResolutionSwitcher;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcherCli;
class SelectResolution
{
    Logger logger;
    public SelectResolution(Logger logger)
    {
        this.logger = logger;
    }

    public void Run(DisplayDeviceDetails selectedDevice)
    {
        // Log the selected display mode details
        foreach (var mode in selectedDevice.DisplayModeDetails)
        {
            logger.LogLine(LogModeDetails(mode));
        }


        logger.Log("Enter the desired display mode id or any key to quit: ");
        var modeIndexInput = Console.ReadLine();

        logger.AddToHistory(modeIndexInput);
        if (!int.TryParse(modeIndexInput, out _))
        {
            logger.SaveLogs();
            return;
        }


        var selectedMode = selectedDevice.DisplayModeDetails.Find(
            (mode) => mode.Index.ToString() == modeIndexInput
        );
        logger.AddToHistoryAndLogLine(selectedMode, "Mode found");

        if (selectedMode == null)
        {
            logger.SaveLogs("Mode not found");
            return;
        }


        logger.LogLine(@$"Selected Device and mode:",
                       LogDeviceDetails(selectedDevice, true),
                       "\n",
                       LogModeDetails(selectedMode),
                       "\n");


        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatus.Successful)
        {
            logger.SaveLogs("Test failed");
            return;
        }


        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
