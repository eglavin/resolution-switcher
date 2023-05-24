using ResolutionSwitcher;
using static ResolutionSwitcher.DisplayDevices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.ChangeDisplaySettings;
using static ResolutionSwitcher.Flags;

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
        selectedDevice.DisplayModeDetails.ForEach(LogModeDetails);


        Console.Write("\nEnter the desired display mode id or any key to quit: ");
        var modeIndexInput = Console.ReadLine();
        if (!int.TryParse(modeIndexInput, out _))
        {
            logger.WriteOutput(new
            {
                selectedDevice,
                modeIndexInput,
            });
            return;
        }


        var selectedMode = selectedDevice.DisplayModeDetails.Find(
            (mode) => mode.Index.ToString() == modeIndexInput
        );
        if (selectedMode == null)
        {
            Console.WriteLine("Mode not found\n");

            logger.WriteOutput(new
            {
                selectedDevice,
                modeIndexInput,
                selectedMode,
            });
            return;
        }


        Console.WriteLine(@$"Mode found

Selected Device and mode:");
        LogDeviceDetails(selectedDevice, true);
        LogModeDetails(selectedMode);
        Console.WriteLine();


        var testStatus = TestDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        if (testStatus != DisplayChangeStatus.Successful)
        {
            Console.WriteLine("Test failed\n");

            logger.WriteOutput(new
            {
                selectedDevice,
                modeIndexInput,
                selectedMode,
                testStatus,
            });
            return;
        }
        Console.WriteLine($"TestDisplayMode: {LogDisplayChangeStatus(testStatus)}");


        var changeStatus = ChangeDisplayMode(selectedDevice.DisplayDevice.DeviceName, selectedMode.DeviceMode);
        Console.WriteLine($"ChangeDisplayMode: {LogDisplayChangeStatus(changeStatus)}");

        var applyStatus = ApplyChanges();
        Console.WriteLine($"ApplyChanges: {LogDisplayChangeStatus(applyStatus)}");
    }
}
