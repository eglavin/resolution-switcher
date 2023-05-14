using System.Linq;
using System.Runtime.InteropServices;
using static ResolutionSwitcherCli.Flags;

namespace ResolutionSwitcherCli;
class DisplayDevices
{
    /// <summary>
    /// The DISPLAY_DEVICE structure receives information about the display device specified by the iDevNum parameter of the EnumDisplayDevices function.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-display_devicea
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DISPLAY_DEVICE
    {
        /// <summary>
        /// Size, in bytes, of the DISPLAY_DEVICE structure. This must be initialized prior to calling EnumDisplayDevices.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int cb;

        /// <summary>
        /// An array of characters identifying the device name. This is either the adapter device or the monitor device.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;

        /// <summary>
        /// An array of characters containing the device context string. This is either a description of the display adapter or of the display monitor.
        /// </summary>        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;

        /// <summary>
        /// Device state flags. It can be any reasonable combination of the following.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DeviceState StateFlags;

        /// <summary>
        /// Not used.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;

        /// <summary>
        /// Reserved.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

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
    private static extern bool EnumDisplayDevices(string lpDevice,
                                                  uint iDevNum,
                                                  ref DISPLAY_DEVICE lpDisplayDevice,
                                                  uint dwFlags);

    public static List<DISPLAY_DEVICE> GetDisplayDevices()
    {
        List<DISPLAY_DEVICE> displays = new();
        try
        {
            DISPLAY_DEVICE displayDevice = new();
            displayDevice.cb = Marshal.SizeOf(displayDevice);

            for (uint id = 0; EnumDisplayDevices(null, id, ref displayDevice, 0); id++)
            {
                displays.Add(displayDevice);
                displayDevice.cb = Marshal.SizeOf(displayDevice);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return displays;
    }

    public static int GetDeviceNumber(string deviceKey)
    {
        try
        {
            string deviceString = deviceKey.Split('\\').Last();
            return Convert.ToInt32(deviceString) + 1; // Zero based index
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
}

