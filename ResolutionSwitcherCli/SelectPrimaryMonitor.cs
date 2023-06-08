﻿using ResolutionSwitcher;
using static ResolutionSwitcher.ChangeDisplaySettings;
using ResolutionSwitcher.Models;
using static ResolutionSwitcher.DisplayDeviceSettings;
using ResolutionSwitcher.Flags;

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
            throw new Exception("No other devices found, primary already set");
        }

        if (!selectedDevice.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop))
        {
            throw new Exception("Device not connected.");
        }

        var currentDeviceDisplayMode = GetCurrentDisplayMode(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
        logger.AddToHistory(currentDeviceDisplayMode);

        var offsetX = currentDeviceDisplayMode.dmPositionX;
        var offsetY = currentDeviceDisplayMode.dmPositionY;
        currentDeviceDisplayMode.dmPositionX = 0;
        currentDeviceDisplayMode.dmPositionY = 0;


        var newPrimaryMonitorStatus = SetPrimaryDisplay(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
        logger.LogLine($"\nSetPrimaryDisplay ({selectedDevice.Name}): {LogDisplayChangeStatus(newPrimaryMonitorStatus)}");

        foreach (var device in otherDevices)
        {
            if (device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop))
            {
                var deviceDisplayMode = GetCurrentDisplayMode(device.DisplayDevice.DeviceName).DeviceMode;

                deviceDisplayMode.dmPositionX -= offsetX;
                deviceDisplayMode.dmPositionY -= offsetY;

                var status = ChangeDisplayMode(device.DisplayDevice.DeviceName, deviceDisplayMode);
                logger.LogLine($"ChangeDisplayMode ({device.Name}): {LogDisplayChangeStatus(status)}");

                logger.AddToHistory(deviceDisplayMode);
            }
        }


        var applyChangesStatus = ApplyChanges();
        logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyChangesStatus)}");
    }
}
