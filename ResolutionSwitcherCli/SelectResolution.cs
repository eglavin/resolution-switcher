﻿using ResolutionSwitcher;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
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


        var currentMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName);
        currentMode.DeviceMode.dmPelsWidth = selectedMode.DeviceMode.dmPelsWidth;
        currentMode.DeviceMode.dmPelsHeight = selectedMode.DeviceMode.dmPelsHeight;
        currentMode.DeviceMode.dmFields = FieldUseFlags.PelsWidth | FieldUseFlags.PelsHeight;

        logger.LogLine(@$"Selected Device and mode:",
                       "\n",
                       GetDeviceDetails(selectedDevice, true),
                       GetModeHead(),
                       GetModeRow(currentMode),
                       "\n");

        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentMode.DeviceMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatus.Successful)
        {
            logger.LogLine("Test failed");
            return;
        }


        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentMode.DeviceMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
