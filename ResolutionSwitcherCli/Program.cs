using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;
using ResolutionSwitcher;
using ResolutionSwitcherCli;


var logger = new Logger("resolution-switcher-cli");


var displayDevices = GetDisplayDevices();
displayDevices.ForEach((device) =>
{
    if (device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
    {
        // Attach the display mode details to the device
        device.DisplayModeDetails = GetDisplayModeDetails(device.DisplayDevice.DeviceName, true);

        LogDeviceDetails(device);
        Console.WriteLine();
    }
});


Console.Write("Enter the desired device id or any key to quit: ");
var deviceIndexInput = Console.ReadLine();
if (!int.TryParse(deviceIndexInput, out _))
{
    logger.WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
    });
    return;
}


var selectedDevice = displayDevices.Find(
    (device) => device.Index.ToString() == deviceIndexInput
);
if (selectedDevice == null)
{
    Console.WriteLine("Device not found\n");

    logger.WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
        selectedDevice,
    });
    return;
}


Console.Write(@$"Device found

-- Options --
1. Set Resolution
2. Set Primary Monitor
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
                           displayDevices.Where((device) => device.Index != selectedDevice.Index).ToList());
        break;
    default:
        Console.WriteLine("Invalid function");

        logger.WriteOutput(new
        {
            displayDevices,
            deviceIndexInput,
            selectedDevice,
            functionInput,
        });
        break;
}

