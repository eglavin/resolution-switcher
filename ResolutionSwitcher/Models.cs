using static ResolutionSwitcher.Structs;

namespace ResolutionSwitcher;
public class Models
{
    public class DisplayDeviceDetails
    {
        public uint Index;
        public string Name { get => DisplayDevice.DeviceName.Split("\\\\.\\").Last(); }
        public int Number { get => Convert.ToInt32(DisplayDevice.DeviceName.Split("DISPLAY").Last()); }
        public string State { get => DisplayDevice.StateFlags.ToString(); }
        public DISPLAY_DEVICE DisplayDevice;
        public List<DeviceModeDetails> DisplayModeDetails = new();

        public DisplayDeviceDetails(uint index, DISPLAY_DEVICE device)
        {
            Index = index;
            DisplayDevice = device;
        }
    }

    public class DeviceModeDetails
    {
        public int Index;
        public int Width { get => DeviceMode.dmPelsWidth; }
        public int Height { get => DeviceMode.dmPelsHeight; }
        public int DisplayFrequency { get => DeviceMode.dmDisplayFrequency; }
        public short BitsPerPixel { get => DeviceMode.dmBitsPerPel; }
        public string Fields { get => DeviceMode.dmFields.ToString(); }
        public DEVMODE DeviceMode;

        public DeviceModeDetails(int index, DEVMODE deviceMode)
        {
            Index = index;
            DeviceMode = deviceMode;
        }
    }
}
