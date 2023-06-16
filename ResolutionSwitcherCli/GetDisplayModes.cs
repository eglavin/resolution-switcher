using ResolutionSwitcher;
using ResolutionSwitcher.Models;
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
            logger.LogLine(GetDeviceDetails(device, false));
            var currentMode = GetDeviceDisplaySettings(device.DisplayDevice.DeviceName);
            logger.LogLine(GetModeHead(), GetModeRow(currentMode), "\n");
        }
    }
}
