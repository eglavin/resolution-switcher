using Windows.Win32.Graphics.Gdi;

namespace ResolutionSwitcher.Models;

public class DeviceModeDetails
{
	public DEVMODEW DeviceMode;

	public uint Index;
	public uint Width { get => DeviceMode.dmPelsWidth; }
	public uint Height { get => DeviceMode.dmPelsHeight; }
	public DEVMODE_DISPLAY_ORIENTATION Orientation { get => DeviceMode.Anonymous1.Anonymous2.dmDisplayOrientation; }
	public short Scale { get => DeviceMode.Anonymous1.Anonymous1.dmScale; }
	public uint DisplayFrequency { get => DeviceMode.dmDisplayFrequency; }
	public uint BitsPerPixel { get => DeviceMode.dmBitsPerPel; }
	public string Fields { get => DeviceMode.dmFields.ToString(); }

	public DeviceModeDetails(uint index, DEVMODEW deviceMode)
	{
		Index = index;
		DeviceMode = deviceMode;


		//var scalingFactor = Math.Round(Decimal.Divide(dm.dmPelsWidth, screen.Bounds.Width), 2);
	}
}