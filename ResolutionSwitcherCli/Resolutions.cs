namespace ResolutionSwitcherCli;
class Resolutions
{
    public static int MIN_WIDTH = 800;
    public static int MIN_HEIGHT = 600;

    public class DisplayResolution
    {
        public List<int> Width { get; set; } = new List<int>();
        public List<int> Height { get; set; } = new List<int>();
    }

    public static void ShowResolutions(string label, List<int> data)
    {
        Console.Write($"{label}: ");
        foreach (var width in data)
        {
            Console.Write($"{width} ");
        }
        Console.WriteLine();
    }
}
