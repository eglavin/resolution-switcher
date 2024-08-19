using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using static ResolutionSwitcher.Functions.ChangeDisplaySettings;
using static ResolutionSwitcher.Functions.DeviceCaps;
using static ResolutionSwitcher.Functions.DeviceContext;
using static ResolutionSwitcher.Functions.DisplayDeviceSettings;

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

		var desktopContext = GetDeviceContext();
		var currentDeviceWidth = GetDeviceWidth(desktopContext);
		ReleaseDeviceContext(desktopContext);

		logger.LogLine($"Current device context width: {currentDeviceWidth}");

		var currentDeviceMode = GetDeviceDisplaySettings(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
		logger.AddToHistory(currentDeviceMode);

		// Modify current device mode
		currentDeviceMode.dmPositionX -= currentDeviceWidth;
		currentDeviceMode.dmFields = DevModeFieldsFlags.Position;

		logger.AddToHistory(currentDeviceMode);


		var testStatus = TestDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
		logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

		if (testStatus != DisplayChangeStatusFlag.Successful)
		{
			throw new Exception("Test failed");
		}


		var changeStatus = UpdateDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
		logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

		var applyStatus = ApplyDisplaySettings();
		logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
	}
}
