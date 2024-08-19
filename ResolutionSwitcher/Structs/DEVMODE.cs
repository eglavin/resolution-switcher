using System.Runtime.InteropServices;
using ResolutionSwitcher.Flags;

namespace ResolutionSwitcher.Structs;

/// <summary>
/// The DEVMODE structure is used for specifying characteristics of display and print devices in the Unicode (wide) character set.
/// </summary>
/// https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-devmodea
/// https://github.com/dotnet/pinvoke/blob/93de9b78bcd8ed84d02901b0556c348fa66257ed/src/Windows.Core/DEVMODE.cs#L14
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DEVMODE
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	public string dmDeviceName;
	public short dmSpecVersion;
	public short dmDriverVersion;
	public short dmSize;
	public short dmDriverExtra;
	public DevModeFieldsFlags dmFields;

	public int dmPositionX;
	public int dmPositionY;
	/// <summary>
	/// Represents the orientation(rotation) of the display. This member can be one of these values:
	/// DMDO_DEFAULT = 0 : Display is in the default orientation.
	/// DMDO_90 = 1 : The display is rotated 90 degrees (measured clockwise) from DMDO_DEFAULT.
	/// DMDO_180 = 2 : The display is rotated 180 degrees (measured clockwise) from DMDO_DEFAULT.
	/// DMDO_270 = 3 : The display is rotated 270 degrees (measured clockwise) from DMDO_DEFAULT.
	/// </summary>
	public int dmDisplayOrientation;
	public int dmDisplayFixedOutput;

	public short dmColor;
	public short dmDuplex;
	public short dmYResolution;
	public short dmTTOption;
	public short dmCollate;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	public string dmFormName;
	public short dmLogPixels;
	public short dmBitsPerPel;
	public int dmPelsWidth;
	public int dmPelsHeight;

	public int dmDisplayFlags;
	public int dmDisplayFrequency;

	public int dmICMMethod;
	public int dmICMIntent;
	public int dmMediaType;
	public int dmDitherType;
	public int dmReserved1;
	public int dmReserved2;

	public int dmPanningWidth;
	public int dmPanningHeight;
};
