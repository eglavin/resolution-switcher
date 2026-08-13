using ResolutionSwitcherLib.Flags;
using ResolutionSwitcherLib.Functions;
using Spectre.Console;
using Spectre.Console.Cli;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;

namespace ResolutionSwitcherCli.Commands;

class AttachCommand : DeviceCommand<DeviceCommandSettings>
{
	protected override int Execute(CommandContext context, DeviceCommandSettings settings, CancellationToken cancellationToken)
	{
		var device = FindDevice(settings.DeviceIndex);
		if (device is null)
		{
			return 1;
		}

		if (((DisplayDeviceFlags)device.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			AnsiConsole.MarkupLine("[red]Device is already attached.[/]");
			return 1;
		}

		var desktopWindow = PInvoke.GetDesktopWindow();
		var desktopContext = PInvoke.GetDC(desktopWindow);
		var currentDeviceWidth = PInvoke.GetDeviceCaps(desktopContext, GET_DEVICE_CAPS_INDEX.HORZRES);
		PInvoke.ReleaseDC(desktopWindow, desktopContext);

		var mode = DisplayDeviceSettings.GetDeviceDisplaySettings(device.DisplayDevice.DeviceName).DeviceMode;
		mode.Anonymous1.Anonymous2.dmPosition.x -= currentDeviceWidth;
		mode.dmFields = DEVMODE_FIELD_FLAGS.DM_POSITION;

		var testStatus = ChangeDisplaySettings.TestDisplaySettings(device.DisplayDevice.DeviceName, mode);
		Formatting.WriteStatus("Test", testStatus);
		if (testStatus != DISP_CHANGE.DISP_CHANGE_SUCCESSFUL)
		{
			return 1;
		}

		var changeStatus = ChangeDisplaySettings.UpdateDisplaySettings(device.DisplayDevice.DeviceName, mode);
		Formatting.WriteStatus("Change", changeStatus);

		var applyStatus = ChangeDisplaySettings.ApplyDisplaySettings();
		Formatting.WriteStatus("Apply", applyStatus);

		return 0;
	}
}
