using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.GetDisplayDevices;

namespace ResolutionSwitcherCli;
class Resolution
{
    static void Main()
    {
        DISPLAY_DEVICE displayDevice = new DISPLAY_DEVICE();
        displayDevice.cb = Marshal.SizeOf(displayDevice);

        try
        {
            for (uint id = 0; EnumDisplayDevices(null, id, ref displayDevice, 0); id++)
            {
                Console.WriteLine($@"ID: {id}
DeviceName: {displayDevice.DeviceName}
DeviceString: {displayDevice.DeviceString}
DeviceID: {displayDevice.DeviceID}
DeviceKey: {displayDevice.DeviceKey}
StateFlags: {displayDevice.StateFlags}
--------------------------------------------------------------------------------");

                displayDevice.cb = Marshal.SizeOf(displayDevice);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}