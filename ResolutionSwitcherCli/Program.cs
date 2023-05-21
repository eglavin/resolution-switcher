using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;
using static ResolutionSwitcher.Logs;


var logOutputFilename = "resolution-switcher-cli";


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
    WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
    },
    logOutputFilename);
    return;
}


var selectedDevice = displayDevices.Find(
    (device) => device.Index.ToString() == deviceIndexInput
);
if (selectedDevice == null)
{
    Console.WriteLine("Device not found\n");

    WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
        selectedDevice,
    },
    logOutputFilename);
    return;
}


Console.WriteLine("Device found\n");
selectedDevice.DisplayModeDetails.ForEach((mode) =>
    LogModeDetails(mode)
);

Console.Write("\nEnter the desired display mode id or any key to quit: ");
var modeIndexInput = Console.ReadLine();
if (!int.TryParse(modeIndexInput, out _))
{
    WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
        selectedDevice,
        modeIndexInput,
    },
    logOutputFilename);
    return;
}


var selectedMode = selectedDevice.DisplayModeDetails.Find(
    (mode) => mode.Index.ToString() == modeIndexInput
);
if (selectedMode == null)
{
    Console.WriteLine("Mode not found\n");

    WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
        selectedDevice,
        modeIndexInput,
        selectedMode,
    },
    logOutputFilename);
    return;
}


Console.WriteLine("Mode found\n");


Console.WriteLine("Selected Device and mode:");
LogDeviceDetails(selectedDevice, true);
LogModeDetails(selectedMode);
Console.WriteLine();


var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
if (testStatus != DisplayChangeStatus.Successful)
{
    Console.WriteLine("Test failed\n");

    WriteOutput(new
    {
        displayDevices,
        deviceIndexInput,
        selectedDevice,
        modeIndexInput,
        selectedMode,
        testStatus,
    },
    logOutputFilename);
    return;
}


Console.WriteLine($"Display Test Status: {testStatus}");


var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
Console.WriteLine($"Display Change Status: {LogDisplaySetting(changeStatus)}");


WriteOutput(new
{
    displayDevices,
    deviceIndexInput,
    selectedDevice,
    modeIndexInput,
    selectedMode,
    testStatus,
    changeStatus,
},
logOutputFilename);
