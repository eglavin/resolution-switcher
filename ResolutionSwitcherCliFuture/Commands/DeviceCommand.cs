using ResolutionSwitcher.Functions;
using ResolutionSwitcher.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ResolutionSwitcherCliFuture.Commands;

abstract class DeviceCommand<TSettings> : Command<TSettings> where TSettings : DeviceCommandSettings
{
	protected static DisplayDeviceDetails? FindDevice(uint index)
	{
		var device = DisplayDevices.GetDisplayDevices().Find(d => d.Index == index);

		if (device is null)
		{
			AnsiConsole.MarkupLineInterpolated($"[red]No display device found with index {index}. Run 'list' to see available devices.[/]");
		}

		return device;
	}
}
