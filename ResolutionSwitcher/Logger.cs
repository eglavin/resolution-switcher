using Newtonsoft.Json;
using System.Reflection;

namespace ResolutionSwitcher;
public class Logger
{
	private string FileName { get; set; } = "resolution-switcher";
	private List<LogHistory> History { get; set; } = new List<LogHistory>();
	private bool OutputToConsole { get; set; } = true;


	public Logger(string? fileName, bool? outputToConsole = true)
	{
		if (fileName != null)
		{
			FileName = fileName;
		}
		OutputToConsole = outputToConsole == true;
	}


	#region Models

	public class LogHistory
	{
		public DateTime DateTime { get; } = DateTime.UtcNow;
		public List<object> Details { get; } = new List<object>();


		public LogHistory(object? message) => Details.Add(message ?? "");
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
		public LogHistory(object?[] message) => Details.AddRange(message);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
	}

	#endregion


	#region Logging Methods

	public void LogLine(params string?[] lines)
	{
		History.Add(new LogHistory(lines));

		if (OutputToConsole)
		{
			foreach (var line in lines)
			{
				if (line == "\n")
				{
					Console.WriteLine();
					continue;
				}
				Console.WriteLine(line);
			}
		}
	}

	public void Log(string? line)
	{
		History.Add(new LogHistory(line));

		if (OutputToConsole)
		{
			Console.Write(line);
		}
	}

	public void AddToHistoryAndLogLine(object? status, params string?[] lines)
	{
		History.Add(new LogHistory(status));

		if (lines.Length > 0)
		{
			LogLine(lines);
		}
	}

	public void AddToHistory(params object?[] status)
	{
		History.Add(new LogHistory(status));
	}

	#endregion


	#region Output Handling

	private void WriteOutput(object output)
	{
		var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss");

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

	public void SaveLogs(params string?[] line)
	{
		if (line.Length > 0)
		{
			LogLine(line);
		}

		WriteOutput(History);
	}

	#endregion

}
