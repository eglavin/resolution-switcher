using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using Windows.Win32.Graphics.Gdi;
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
		if (!((DisplayDeviceFlags) selectedDevice.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			throw new Exception("Device already detached");
		}

		if (((DisplayDeviceFlags) selectedDevice.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.PrimaryDevice))
		{
			// TODO: Set primary monitor to the next available monitor
			throw new Exception("Device is the primary monitor");
		}

		var currentDeviceDisplayMode = GetDeviceDisplaySettings(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
		logger.AddToHistory(currentDeviceDisplayMode);

		currentDeviceDisplayMode.dmPelsWidth = 0;
		currentDeviceDisplayMode.dmPelsHeight = 0;
		currentDeviceDisplayMode.dmFields = DEVMODE_FIELD_FLAGS.DM_POSITION |
																				DEVMODE_FIELD_FLAGS.DM_PELSWIDTH |
																				DEVMODE_FIELD_FLAGS.DM_PELSHEIGHT;

		logger.AddToHistory(currentDeviceDisplayMode);

		var testStatus = TestDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
		logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

		if (testStatus != DISP_CHANGE.DISP_CHANGE_SUCCESSFUL)
		{
			throw new Exception("Test failed");
		}

		var changeStatus = UpdateDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceDisplayMode);
		logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

		var applyStatus = ApplyDisplaySettings();
		logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
	}
}
