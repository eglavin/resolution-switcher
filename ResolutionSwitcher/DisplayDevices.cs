using System.Runtime.InteropServices;
using static ResolutionSwitcher.DisplayDeviceSettings;
using static ResolutionSwitcher.Flags;

namespace ResolutionSwitcher;
public class DisplayDevices
{
    /// <summary>
    /// The DISPLAY_DEVICE structure receives information about the display device specified by the iDevNum parameter of the EnumDisplayDevices function.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-display_devicea
    /// https://github.com/dotnet/pinvoke/blob/93de9b78bcd8ed84d02901b0556c348fa66257ed/src/Windows.Core/DISPLAY_DEVICE.cs
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
        public DisplayDeviceFlags StateFlags;

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


    public class DisplayDeviceDetails
    {
        public DisplayDeviceDetails(uint index, DISPLAY_DEVICE device)
        {
            Index = index;
            Name = device.DeviceName.Split("\\\\.\\").Last();
            Number = Convert.ToInt32(device.DeviceName.Split("DISPLAY").Last());
            State = device.StateFlags.ToString();
            DisplayDevice = device;
        }

        public uint Index;
        public string Name;
        public int Number;
        public string State;
        public DISPLAY_DEVICE DisplayDevice;
        public List<DeviceModeDetails> DisplayModeDetails = new List<DeviceModeDetails>();
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
