using ResolutionSwitcher.Structs;

namespace ResolutionSwitcher.Models;

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
