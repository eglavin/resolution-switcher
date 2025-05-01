using ResolutionSwitcher;
using ResolutionSwitcher.Models;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32;
using static ResolutionSwitcher.Functions.DisplayDeviceSettings;
using static ResolutionSwitcherCli.Utils;

namespace ResolutionSwitcherCli;
class GetDisplayModes
{
	Logger logger;
	public GetDisplayModes(Logger logger)
	{
		this.logger = logger;
	}

	public void Run(List<DisplayDeviceDetails> displayDevices)
	{
		foreach (var device in displayDevices)
		{
			logger.LogLine(GetDeviceDetails(device, true));
			var currentMode = GetDeviceDisplaySettings(device.DisplayDevice.DeviceName);
			logger.LogLine(GetModeHead(), GetModeRow(currentMode), "\n");


			//var desktopWindow = PInvoke.GetDesktopWindow();
			//var desktopContext = PInvoke.GetDC(desktopWindow);

			//var DESKTOPVERTRES = PInvoke.GetDeviceCaps(desktopContext, GET_DEVICE_CAPS_INDEX.DESKTOPVERTRES);
			//var VERTRES = PInvoke.GetDeviceCaps(desktopContext, GET_DEVICE_CAPS_INDEX.VERTRES);

			//var dpi = PInvoke.GetDpiForWindow(desktopWindow);

			//PInvoke.ReleaseDC(desktopWindow, desktopContext);


			//logger.LogLine($"DESKTOPVERTRES : {DESKTOPVERTRES}");
			//logger.LogLine($"VERTRES : {VERTRES }");
			//logger.LogLine($"dpi : {dpi}");
		}
	}
}
