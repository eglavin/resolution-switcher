namespace ResolutionSwitcher.Models;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
public class Mode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Display> Displays { get; set; }
}
