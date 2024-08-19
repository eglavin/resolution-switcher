using ResolutionSwitcher.Structs;

namespace ResolutionSwitcher.Models;

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