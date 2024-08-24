using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ResolutionSwitcher.Models;

public class ResolutionSwitcherContext : DbContext
{
	public DbSet<Mode> Modes { get; set; }
	public DbSet<Display> Displays { get; set; }

	public string DbPath { get; }

	public ResolutionSwitcherContext()
	{
		string executionLocation = Assembly.GetExecutingAssembly().Location;
		string folder = Path.GetDirectoryName(executionLocation) + "";
		DbPath = Path.Join(folder, "ResolutionSwitcher.db");

		Database.Migrate();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseSqlite($"Data Source={DbPath}");
	}
}
