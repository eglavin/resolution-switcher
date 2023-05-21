namespace ResolutionSwitcher;
public class Flags
{
    [Flags()]
    public enum DisplayDeviceStateFlags : int
    {
        AttachedToDesktop = 1,
        MultiDriver = 2,
        PrimaryDevice = 4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 8,
        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 10,
        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 20,
        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 8000000,
        Remote = 4000000,
        Disconnect = 2000000
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
        None = 0,
        UpdateRegistry = 1,
        Test = 2,
        Fullscreen = 4,
        Global = 8,
        SetPrimary = 10,
        VideoParameters = 20,
        EnableUnsafeModes = 100,
        DisableUnsafeModes = 200,
        Reset = 40000000,
        ResetEx = 20000000,
        NoReset = 10000000
    }
}
