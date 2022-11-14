using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.DisplayDevices;
using static ResolutionSwitcherCli.DisplaySettings;
using static ResolutionSwitcherCli.Flags;

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
--------------------------------------------------------------------------------");

                var displayModes = GetDisplaySettings(display.DeviceName);

                foreach (var mode in displayModes)
                {
                    Console.WriteLine($@"Width: {mode.dmPelsWidth}
Height: {mode.dmPelsHeight}
---------------------------");
                }
            }

        }
    }
}