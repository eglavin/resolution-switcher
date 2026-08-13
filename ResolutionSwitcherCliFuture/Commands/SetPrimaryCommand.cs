using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Functions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ResolutionSwitcherCliFuture.Commands;

class SetPrimaryCommand : DeviceCommand<DeviceCommandSettings>
{
	protected override int Execute(CommandContext context, DeviceCommandSettings settings, CancellationToken cancellationToken)
	{
		var device = FindDevice(settings.DeviceIndex);
		if (device is null)
		{
			return 1;
		}

		if (!((DisplayDeviceFlags)device.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop))
		{
			AnsiConsole.MarkupLine("[red]Device is not attached to the desktop.[/]");
			return 1;
		}

		var otherDevices = DisplayDevices.GetDisplayDevices()
			.Where(other => ((DisplayDeviceFlags)other.DisplayDevice.StateFlags).HasFlag(DisplayDeviceFlags.AttachedToDesktop) && other.Index != device.Index)
			.ToList();

		if (otherDevices.Count == 0)
		{
			AnsiConsole.MarkupLine("[yellow]No other attached devices found, primary already set.[/]");
			return 0;
		}

		var currentMode = DisplayDeviceSettings.GetDeviceDisplaySettings(device.DisplayDevice.DeviceName).DeviceMode;
		var offsetX = currentMode.Anonymous1.Anonymous2.dmPosition.x;
		var offsetY = currentMode.Anonymous1.Anonymous2.dmPosition.y;
		currentMode.Anonymous1.Anonymous2.dmPosition.x = 0;
		currentMode.Anonymous1.Anonymous2.dmPosition.y = 0;

		var primaryStatus = ChangeDisplaySettings.SetPrimaryDisplay(device.DisplayDevice.DeviceName, currentMode);
		Formatting.WriteStatus($"Set primary ({device.Name})", primaryStatus);

		foreach (var other in otherDevices)
		{
			var otherMode = DisplayDeviceSettings.GetDeviceDisplaySettings(other.DisplayDevice.DeviceName).DeviceMode;
			otherMode.Anonymous1.Anonymous2.dmPosition.x -= offsetX;
			otherMode.Anonymous1.Anonymous2.dmPosition.y -= offsetY;

			var status = ChangeDisplaySettings.UpdateDisplaySettings(other.DisplayDevice.DeviceName, otherMode);
			Formatting.WriteStatus($"Reposition ({other.Name})", status);
		}

		var applyStatus = ChangeDisplaySettings.ApplyDisplaySettings();
		Formatting.WriteStatus("Apply", applyStatus);

		return 0;
	}
}
