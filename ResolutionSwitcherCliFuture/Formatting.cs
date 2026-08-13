using ResolutionSwitcher.Models;
using Spectre.Console;
using Windows.Win32.Graphics.Gdi;

namespace ResolutionSwitcherCliFuture;

static class Formatting
{
	public static Table DevicesTable(List<DisplayDeviceDetails> devices)
	{
		var table = new Table().Border(TableBorder.Rounded);
		table.AddColumns("Index", "Name", "State", "Resolution", "Frequency", "Scale");

		foreach (var device in devices)
		{
			var mode = ResolutionSwitcher.Functions.DisplayDeviceSettings.GetDeviceDisplaySettings(device.DisplayDevice.DeviceName);
			var scale = ResolutionSwitcher.Functions.DisplayScaling.GetDisplayScaleInfo(device.DisplayDevice.DeviceName);

			table.AddRow(
				device.Index.ToString(),
				device.Name,
				device.State,
				$"{mode.Width}x{mode.Height}",
				$"{mode.DisplayFrequency}Hz",
				scale is null ? "-" : $"{scale.Current}%"
			);
		}

		return table;
	}

	public static Table ModesTable(List<DeviceModeDetails> modes)
	{
		var table = new Table().Border(TableBorder.Rounded);
		table.AddColumns("Index", "Width", "Height", "Frequency");

		foreach (var mode in modes)
		{
			table.AddRow(mode.Index.ToString(), mode.Width.ToString(), mode.Height.ToString(), $"{mode.DisplayFrequency}Hz");
		}

		return table;
	}

	public static Table ScaleTable(DisplayScaleInfo scale)
	{
		var table = new Table().Border(TableBorder.Rounded);
		table.AddColumns("Current", "Recommended", "Minimum", "Maximum", "Available");

		table.AddRow(
			$"{scale.Current}%",
			$"{scale.Recommended}%",
			$"{scale.Minimum}%",
			$"{scale.Maximum}%",
			string.Join(", ", scale.AvailableScales.Select(s => $"{s}%"))
		);

		return table;
	}

	public static void WriteStatus(string label, DISP_CHANGE status)
	{
		var message = ResolutionSwitcher.Functions.ChangeDisplaySettings.LogDisplayChangeStatus(status);
		var color = status switch
		{
			DISP_CHANGE.DISP_CHANGE_SUCCESSFUL => "green",
			DISP_CHANGE.DISP_CHANGE_RESTART => "yellow",
			_ => "red"
		};

		AnsiConsole.MarkupLineInterpolated($"{label}: [{color}]{message}[/]");
	}

	public static void WriteResult(string label, bool success)
	{
		var color = success ? "green" : "red";
		var message = success ? "Success" : "Failed";
		AnsiConsole.MarkupLineInterpolated($"{label}: [{color}]{message}[/]");
	}
}
