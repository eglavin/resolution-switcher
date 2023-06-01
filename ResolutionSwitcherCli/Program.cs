using ResolutionSwitcher;
using ResolutionSwitcherCli;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcherCli.Utils;


var logger = new Logger("resolution-switcher-cli");
try
{

    logger.Log(@$"-- Options --
0. Get All Device Settings
1. Set Resolution
2. Set Primary Monitor
3. Detach Monitor
4. Attach Monitor

Enter the function you want to run: ");
    var functionInput = Console.ReadLine();
    Console.WriteLine();


    var displayDevices = GetDisplayDevices();
    displayDevices.ForEach((device) =>
    {
        // Attach the display mode details to the device
        device.DisplayModeDetails = GetDisplayModeDetails(device.DisplayDevice.DeviceName, true);
        if (functionInput != "0")
        {
            logger.LogLine(GetDeviceDetails(device), "\n");
        }
    });
    logger.AddToHistory(displayDevices);


    switch (functionInput)
    {
        case "0":
            {
                var displayMode = new GetDisplayModes(logger);
                displayMode.Run(displayDevices);
                break;
            }
        case "1":
            {
                var selectedDevice = AskUserToSelectDevice(displayDevices, logger);
                var resolution = new SelectResolution(logger);
                resolution.Run(selectedDevice);
                break;
            }
        case "2":
            {
                var selectedDevice = AskUserToSelectDevice(displayDevices, logger);
                var primaryMonitor = new SelectPrimaryMonitor(logger);
                primaryMonitor.Run(selectedDevice,
                                   displayDevices.Where((device) => device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceFlags.AttachedToDesktop) &&
                                                                    device.Index != selectedDevice.Index)
                                                 .ToList());
                break;
            }
        case "3":
            {
                var selectedDevice = AskUserToSelectDevice(displayDevices, logger);

                var detachMonitor = new DetachMonitor(logger);
                detachMonitor.Run(selectedDevice);
                break;
            }
        case "4":
            {
                var selectedDevice = AskUserToSelectDevice(displayDevices, logger);
                var attachMonitor = new AttachMonitor(logger);
                attachMonitor.Run(selectedDevice);
                break;
            }
        default:
            {
                logger.LogLine("Invalid function:", functionInput);
                break;
            }
    }
}
catch (Exception ex)
{
    logger.AddToHistory(ex);
    Console.WriteLine(ex.Message);
}

logger.SaveLogs();
