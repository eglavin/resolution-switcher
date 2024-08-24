using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using static ResolutionSwitcher.Functions.ChangeDisplaySettings;
using static ResolutionSwitcher.Functions.DisplayDeviceSettings;

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

		if (!((DisplayDeviceFlags) selectedDevice.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			throw new Exception("Device not connected.");
		}

		var currentDeviceDisplayMode = GetDeviceDisplaySettings(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
		logger.AddToHistory(currentDeviceDisplayMode);

		var offsetX = currentDeviceDisplayMode.Anonymous1.Anonymous2.dmPosition.x;
		var offsetY = currentDeviceDisplayMode.Anonymous1.Anonymous2.dmPosition.y;
		currentDeviceDisplayMode.Anonymous1.Anonymous2.dmPosition.x = 0;
		currentDeviceDisplayMode.Anonymous1.Anonymous2.dmPosition.y = 0;


		var newPrimaryMonitorStatus = SetPrimaryDisplay(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
		logger.LogLine($"\nSetPrimaryDisplay ({selectedDevice.Name}): {LogDisplayChangeStatus(newPrimaryMonitorStatus)}");

		foreach (var device in otherDevices)
		{
			if (((DisplayDeviceFlags) device.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
			{
				var deviceDisplayMode = GetDeviceDisplaySettings(device.DisplayDevice.DeviceName).DeviceMode;

				deviceDisplayMode.Anonymous1.Anonymous2.dmPosition.x -= offsetX;
				deviceDisplayMode.Anonymous1.Anonymous2.dmPosition.y -= offsetY;

				var status = UpdateDisplaySettings(device.DisplayDevice.DeviceName, deviceDisplayMode);
				logger.LogLine($"ChangeDisplayMode ({device.Name}): {LogDisplayChangeStatus(status)}");

				logger.AddToHistory(deviceDisplayMode);
			}
		}


		var applyChangesStatus = ApplyDisplaySettings();
		logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyChangesStatus)}");
	}
}
