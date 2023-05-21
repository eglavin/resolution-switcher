using Newtonsoft.Json;
using System.Reflection;

namespace ResolutionSwitcher;
public class Logger
{
    string FileName { get; set; } = "resolution-switcher";

    public Logger(string? fileName)
    {
        if (fileName != null)
        {
            FileName = fileName;
        }

    }

    public void WriteOutput(object output)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\logs";
        var fileDirectory = $"{directory}\\{timestamp}_{FileName}.json";

        try
        {
            var content = JsonConvert.SerializeObject(output, Formatting.Indented);

            Directory.CreateDirectory(directory);
            if (!Directory.Exists(directory))
            {
                throw new Exception("Unable to create directory.");
            }
            if (File.Exists(fileDirectory))
            {
                throw new Exception($"Unable to write log, File already exists: {fileDirectory}.");
            }

            File.WriteAllText(fileDirectory, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}