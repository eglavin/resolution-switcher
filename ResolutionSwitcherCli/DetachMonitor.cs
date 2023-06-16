﻿using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using static ResolutionSwitcher.Functions.ChangeDisplaySettings;
using static ResolutionSwitcher.Functions.DisplayDeviceSettings;

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
        currentDeviceDisplayMode.dmFields = DevModeFieldsFlags.Position |
                                            DevModeFieldsFlags.PelsWidth |
                                            DevModeFieldsFlags.PelsHeight;

        logger.AddToHistory(currentDeviceDisplayMode);

        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

        if (testStatus != DisplayChangeStatusFlag.Successful)
        {
            throw new Exception("Test failed");
        }

        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
