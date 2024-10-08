using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32;
using static ResolutionSwitcher.Functions.ChangeDisplaySettings;
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
		if (((DisplayDeviceFlags) selectedDevice.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			throw new Exception("Device already attached");
		}

		var desktopWindow = PInvoke.GetDesktopWindow();
		var desktopContext = PInvoke.GetDC(desktopWindow);
		var currentDeviceWidth = PInvoke.GetDeviceCaps(desktopContext, GET_DEVICE_CAPS_INDEX.HORZRES);
		PInvoke.ReleaseDC(desktopWindow, desktopContext);

		logger.LogLine($"Current device context width: {currentDeviceWidth}");

		var currentDeviceMode = GetDeviceDisplaySettings(selectedDevice.DisplayDevice.DeviceName).DeviceMode;
		logger.AddToHistory(currentDeviceMode);

		// Modify current device mode
		currentDeviceMode.Anonymous1.Anonymous2.dmPosition.x -= currentDeviceWidth;
		currentDeviceMode.dmFields = DEVMODE_FIELD_FLAGS.DM_POSITION;

		logger.AddToHistory(currentDeviceMode);


		var testStatus = TestDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
		logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

		if (testStatus != DISP_CHANGE.DISP_CHANGE_SUCCESSFUL)
		{
			throw new Exception("Test failed");
		}


		var changeStatus = UpdateDisplaySettings(selectedDevice.DisplayDevice.DeviceName, currentDeviceMode);
		logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

		var applyStatus = ApplyDisplaySettings();
		logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
	}
}
