namespace ResolutionSwitcher.Flags;

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
