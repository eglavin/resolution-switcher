using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcher.Logs;

var displayDevice = GetDisplayDevices();
displayDevice.ForEach((device) =>
{
    if (device.DisplayDevice.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
    {
        var displayModeDetails = GetDisplayModeDetails(device.DisplayDevice.DeviceName);
        device.DisplayModeDetails = displayModeDetails;

        LogDeviceDetails(device, true);
    }
});

Console.Write("Enter the desired display id or any key to quit: ");
var displayIndexInput = Console.ReadLine();
if (!int.TryParse(displayIndexInput, out _))
{
    return;
}

var deviceDetails = displayDevice.Find(
    (d) => d.Index.ToString() == displayIndexInput
);

if (deviceDetails == null)
{
    Console.WriteLine("\nDisplay not found");
    return;
}
else
{
    Console.WriteLine("\nDisplay found\n");
    deviceDetails.DisplayModeDetails.ForEach((details) => LogModeDetails(details));

    Console.Write("Enter the desired display mode id or any key to quit: ");
    var displayModeIndexInput = Console.ReadLine();
    if (!int.TryParse(displayModeIndexInput, out _))
    {
        return;
    }

    var deviceMode = deviceDetails.DisplayModeDetails.Find(
        (d) => d.Index.ToString() == displayModeIndexInput
    );

    if (deviceMode == null)
    {
        Console.WriteLine("\nMode not found");
        return;
    }
    else
    {
        Console.WriteLine("\nMode found");
        LogModeDetails(deviceMode);

        var test = TestDisplayMode(deviceDetails.Name, deviceMode.DeviceMode);

        Console.WriteLine($"Output: {test}");
        if (test != DisplayChangeStatus.Failed)
        {
            var change = ChangeDisplayMode(deviceDetails.Name, deviceMode.DeviceMode);
            Console.WriteLine($"Change: {change}");
        }
    }

    WriteOutput(new
    {
        deviceDetails,
        deviceMode
    },
    "resolution-switcher");
}

Console.WriteLine("Press any button to quit.");
Console.ReadKey();
