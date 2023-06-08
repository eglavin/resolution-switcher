namespace ResolutionSwitcher.Models;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
public class Display
{
    public int Id { get; set; }
    public int ModeId { get; set; }
    public string DisplayName { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Frequency { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool IsPrimary { get; set; }
}
