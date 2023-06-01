namespace ResolutionSwitcher;
public class Flags
{
    [Flags()]
    public enum DisplayDeviceFlags : int
    {
        Detached = 0x0,
        AttachedToDesktop = 0x1,
        MultiDriver = 0x2,
        PrimaryDevice = 0x4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 0x8,
        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 0x10,
        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 0x20,
        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 0x8000000,
        Remote = 0x4000000,
        Disconnect = 0x2000000
    }

    [Flags()]
    public enum DisplayChangeStatus : int
    {
        Successful = 0,
        Restart = 1,
        Failed = -1,
        BadMode = -2,
        NotUpdated = -3,
        BadFlags = -4,
        BadParam = -5,
        BadDualView = -6
    }

    [Flags()]
    public enum ChangeDisplaySettingsFlags : uint
    {
        None = 0x0,
        UpdateRegistry = 0x1,
        Test = 0x2,
        Fullscreen = 0x4,
        Global = 0x8,
        SetPrimary = 0x10,
        VideoParameters = 0x20,
        EnableUnsafeModes = 0x100,
        DisableUnsafeModes = 0x200,
        Reset = 0x40000000,
        ResetEx = 0x20000000,
        NoReset = 0x10000000
    }

    [Flags()]
    public enum FieldUseFlags : uint
    {
        None = 0,
        Orientation = 0x1,
        PaperSize = 0x2,
        PaperLength = 0x4,
        PaperWidth = 0x8,
        Scale = 0x10,
        Position = 0x20,
        NUP = 0x40,
        DisplayOrientation = 0x80,
        Copies = 0x100,
        DefaultSource = 0x200,
        PrintQuality = 0x400,
        Color = 0x800,
        Duplex = 0x1000,
        YResolution = 0x2000,
        TTOption = 0x4000,
        Collate = 0x8000,
        FormName = 0x10000,
        LogPixels = 0x20000,
        BitsPerPixel = 0x40000,
        PelsWidth = 0x80000,
        PelsHeight = 0x100000,
        DisplayFlags = 0x200000,
        DisplayFrequency = 0x400000,
        ICMMethod = 0x800000,
        ICMIntent = 0x1000000,
        MediaType = 0x2000000,
        DitherType = 0x4000000,
        PanningWidth = 0x8000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }

    [Flags()]
    public enum EnumDisplayModeFlags : int
    {
        /// <summary>Retrieve the current display mode.</summary>
        CurrentSettings = -1,
        /// <summary>Retrieve the current display mode saved to the registry.</summary>
        RegistrySettings = -2,
    }

    [Flags()]
    public enum DeviceCap : int
    {
        DriverVersion = 0,
        Technology = 2,
        HorzSize = 4,
        VertSize = 6,
        HorzRes = 8,
        VertRes = 10,
        BitsPixel = 12,
        Planes = 14,
        NumBrushes = 16,
        NumPens = 18,
        NumMarkers = 20,
        NumFonts = 22,
        NumColors = 24,
        PDeviceSize = 26,
        CurveCaps = 28,
        LineCaps = 30,
        PolygonalCaps = 32,
        TextCaps = 34,
        ClipCaps = 36,
        RasterCaps = 38,
        AspectX = 40,
        AspectY = 42,
        AspectXY = 44,
        ShadeBlendCaps = 45,
        LogPixelsX = 88,
        LogPixelsY = 90,
        SizePalette = 104,
        NumReserved = 106,
        ColorRes = 108,
        PhysicalWidth = 110,
        PhysicalHeight = 111,
        PhysicalOffsetX = 112,
        PhysicalOffsetY = 113,
        ScalingFactorX = 114,
        ScalingFactorY = 115,
        VRefresh = 116,
        DesktopVertRes = 117,
        DesktopHorzRes = 118,
        BltAlignment = 119
    }
}
