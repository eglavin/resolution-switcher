using ResolutionSwitcherLib.Flags;
using ResolutionSwitcherLib.Functions;
using Spectre.Console;
using Spectre.Console.Cli;
using Windows.Win32.Graphics.Gdi;

namespace ResolutionSwitcherCli.Commands;

class DetachCommand : DeviceCommand<DeviceCommandSettings>
{
	protected override int Execute(CommandContext context, DeviceCommandSettings settings, CancellationToken cancellationToken)
	{
		var device = FindDevice(settings.DeviceIndex);
		if (device is null)
		{
			return 1;
		}

		var flags = (DisplayDeviceFlags)device.DisplayDevice.StateFlags;
		if (!flags.HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			AnsiConsole.MarkupLine("[red]Device is already detached.[/]");
			return 1;
		}

		if (flags.HasFlag(DisplayDeviceFlags.PrimaryDevice))
		{
			AnsiConsole.MarkupLine("[red]Cannot detach the primary device. Set another device as primary first.[/]");
			return 1;
		}

		var mode = DisplayDeviceSettings.GetDeviceDisplaySettings(device.DisplayDevice.DeviceName).DeviceMode;
		mode.dmPelsWidth = 0;
		mode.dmPelsHeight = 0;
		mode.dmFields = DEVMODE_FIELD_FLAGS.DM_POSITION |
						 DEVMODE_FIELD_FLAGS.DM_PELSWIDTH |
						 DEVMODE_FIELD_FLAGS.DM_PELSHEIGHT;

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
