using Newtonsoft.Json;
using System.Reflection;

namespace ResolutionSwitcher;
public class Logs
{
    public static void WriteOutput(object output, string fileName)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\logs";
        var fileDirectory = $"{directory}\\{timestamp}_{fileName}.json";

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