using ResolutionSwitcher;
using ResolutionSwitcher.Flags;
using ResolutionSwitcher.Models;
using static ResolutionSwitcher.Functions.ChangeDisplaySettings;
using static ResolutionSwitcher.Functions.DisplayDeviceSettings;
using static ResolutionSwitcherCli.Utils;

namespace ResolutionSwitcherCli;
class SelectResolution
{
	Logger logger;
	public SelectResolution(Logger logger)
	{
		this.logger = logger;
	}

	public void Run(DisplayDeviceDetails selectedDevice)
	{
		logger.LogLine("\nSelect the id of the mode you want to apply: ", GetModeHead());
		// Log the selected display mode details
		foreach (var mode in selectedDevice.DisplayModeDetails)
		{
			logger.LogLine(GetModeRow(mode));
		}


		logger.Log("\nEnter the desired display mode id: ");
		var modeIndexInput = Console.ReadLine();

		logger.AddToHistory(modeIndexInput);
		if (!int.TryParse(modeIndexInput, out _))
		{
			throw new Exception($"Unable to parse input: {modeIndexInput}");
		}


		var selectedMode = selectedDevice.DisplayModeDetails.Find(
			(mode) => mode.Index.ToString() == modeIndexInput
		);
		logger.AddToHistoryAndLogLine(selectedMode, "Mode found");

		if (selectedMode == null)
		{
			throw new Exception("Mode not found");
		}


		var newMode = GetDeviceDisplaySettings(selectedDevice.DisplayDevice.DeviceName);
		newMode.DeviceMode.dmPelsWidth = selectedMode.DeviceMode.dmPelsWidth;
		newMode.DeviceMode.dmPelsHeight = selectedMode.DeviceMode.dmPelsHeight;

		logger.LogLine(@$"Selected Device and mode:",
					   "\n",
					   GetDeviceDetails(selectedDevice, true),
					   GetModeHead(),
					   GetModeRow(newMode),
					   "\n");

		var testStatus = TestDisplaySettings(selectedDevice.DisplayDevice.DeviceName, newMode.DeviceMode);
		logger.LogLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");

		if (testStatus != DisplayChangeStatusFlag.Successful)
		{
			throw new Exception("Test failed");
		}


		var changeStatus = UpdateDisplaySettings(selectedDevice.DisplayDevice.DeviceName, newMode.DeviceMode);
		logger.LogLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

		var applyStatus = ApplyDisplaySettings();
		logger.LogLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
	}
}
