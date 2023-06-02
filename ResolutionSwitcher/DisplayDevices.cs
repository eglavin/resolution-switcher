using System.Runtime.InteropServices;
using static ResolutionSwitcher.Models;
using static ResolutionSwitcher.Structs;

namespace ResolutionSwitcher;
public class DisplayDevices
{
    /// <summary>
    /// The EnumDisplayDevices function lets you obtain information about the display devices in the current session.
    /// </summary>
    /// http://pinvoke.net/default.aspx/user32/EnumDisplayDevices.html
    /// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesa
    /// <param name="lpDevice">A pointer to the device name. If NULL, function returns information for the display adapter(s) on the machine, based on iDevNum.</param>
    /// <param name="iDevNum">An index value that specifies the display device of interest.</param>
    /// <param name="lpDisplayDevice">A pointer to a DISPLAY_DEVICE structure that receives information about the display device specified by iDevNum.</param>
    /// <param name="dwFlags">Set this flag to EDD_GET_DEVICE_INTERFACE_NAME (0x00000001) to retrieve the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis.</param>
    /// <returns>If the function fails, the return value is zero. The function fails if iDevNum is greater than the largest device index.</returns>
    [DllImport("user32.dll")]
    private static extern bool EnumDisplayDevices(string? lpDevice,
                                                  uint iDevNum,
                                                  ref DISPLAY_DEVICE lpDisplayDevice,
                                                  uint dwFlags);

    public static List<DisplayDeviceDetails> GetDisplayDevices()
    {
        List<DisplayDeviceDetails> displayDevices = new();
        try
        {
            DISPLAY_DEVICE device = new();
            device.cb = Marshal.SizeOf(device);

            for (uint index = 0; EnumDisplayDevices(null, index, ref device, 0); index++)
            {
                displayDevices.Add(new DisplayDeviceDetails(index, device));
                device.cb = Marshal.SizeOf(device);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return displayDevices;
    }
}
