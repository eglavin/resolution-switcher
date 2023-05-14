using static ResolutionSwitcherCli.DisplayDevices;
using static ResolutionSwitcherCli.DisplaySettings;
using static ResolutionSwitcherCli.Flags;
using static ResolutionSwitcherCli.Resolutions;

namespace ResolutionSwitcherCli;
class Resolution
{
    static void Main()
    {
        var displays = GetDisplayDevices();

        foreach ((DISPLAY_DEVICE display, int key) in displays.Select((val, i) => (val, i)))
        {
            if (display.StateFlags.HasFlag(DeviceState.AttachedToDesktop))
            {
                Console.WriteLine($@"ID: {key}
Device Name: {display.DeviceName}
Device String: {display.DeviceString}
Device ID: {display.DeviceID}
Device Key: {display.DeviceKey}
State Flags: {display.StateFlags}
Device Number: {GetDeviceNumber(display.DeviceKey)}");

                var displaySizes = GetDisplaySizes(display.DeviceName);

                ShowResolutions("Supported Widths", displaySizes.Width);
                ShowResolutions("Supported Heights", displaySizes.Height);

                Console.WriteLine("--------------------------------------------------------------------------------");
            }
        }
    }
}