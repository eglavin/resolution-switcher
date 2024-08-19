namespace ResolutionSwitcher.Flags;

[Flags()]
public enum DisplayModeFlag : int
{
	/// <summary>Retrieve the current display mode.</summary>
	CurrentSettings = -1,
	/// <summary>Retrieve the current display mode saved to the registry.</summary>
	RegistrySettings = -2,
}
