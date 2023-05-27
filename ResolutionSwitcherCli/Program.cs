using ResolutionSwitcher;
using ResolutionSwitcherCli;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcherCli.Utils;


var logger = new Logger("resolution-switcher-cli");

var displayDevices = GetDisplayDevices();
displayDevices.ForEach((device) =>
{
    // Attach the display mode details to the device
    device.DisplayModeDetails = GetDisplayModeDetails(device.DisplayDevice.DeviceName, true);
    logger.LogLine(GetDeviceDetails(device), "\n");
});

logger.AddToHistory(displayDevices);


logger.Log("Enter the desired device id or any key to quit: ");
var deviceIndexInput = Console.ReadLine();
logger.AddToHistory(deviceIndexInput);

if (!int.TryParse(deviceIndexInput, out _))
{
    logger.SaveLogs();
    return;
}


var selectedDevice = displayDevices.Find(
    (device) => device.Index.ToString() == deviceIndexInput
);
logger.AddToHistory(selectedDevice);

if (selectedDevice == null)
{
    logger.SaveLogs("Device not found");
    return;
}


logger.Log(@$"Device found

-- Options --
1. Set Resolution
2. Set Primary Monitor
3. Detach Monitor
Enter the function you want to run: ");
var functionInput = Console.ReadLine();

switch (functionInput)
{
    case "1":
        var resolution = new SelectResolution(logger);
        resolution.Run(selectedDevice);
        break;
    case "2":
        var primaryMonitor = new SelectPrimaryMonitor(logger);
        primaryMonitor.Run(selectedDevice,
                           displayDevices.Where((device) => device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop) &&
                                                            device.Index != selectedDevice.Index)
                                         .ToList());
        break;
    case "3":
        var detachMonitor = new DetachMonitor(logger);
        detachMonitor.Run(selectedDevice);
        break;
    default:
        logger.LogLine("\nInvalid function:", functionInput);
        break;
}

logger.SaveLogs();
