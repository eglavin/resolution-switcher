using ResolutionSwitcher;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcherCli.Utils;

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
        logger.LogLine("\nSelect the id of the mode you want to apply: ", GetModeHead());
        // Log the selected display mode details
        foreach (var mode in selectedDevice.DisplayModeDetails)
        {
            logger.LogLine(GetModeRow(mode));
        }


        logger.Log("\nEnter the desired display mode id or any key to quit: ");
        var modeIndexInput = Console.ReadLine();

        logger.AddToHistory(modeIndexInput);
        if (!int.TryParse(modeIndexInput, out _))
        {
            return;
        }


        var selectedMode = selectedDevice.DisplayModeDetails.Find(
            (mode) => mode.Index.ToString() == modeIndexInput
        );
        logger.AddToHistoryAndLogLine(selectedMode, "Mode found");

        if (selectedMode == null)
        {
            logger.LogLine("Mode not found");
            return;
        }


        logger.LogLine(@$"Selected Device and mode:",
                       "\n",
                       GetDeviceDetails(selectedDevice, true),
                       GetModeHead(),
                       GetModeRow(selectedMode),
                       "\n");


        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatus.Successful)
        {
            logger.LogLine("Test failed");
            return;
        }


        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
