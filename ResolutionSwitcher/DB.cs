using Microsoft.Data.Sqlite;
using static ResolutionSwitcher.Structs;

namespace ResolutionSwitcher;
public class DB
{
    private SqliteConnection _connection;

    public DB()
    {
        _connection = new SqliteConnection("Data Source=ResolutionSwitcher.db");
        _connection.Open();

        CreateTables();
    }

    public void Close()
    {
        _connection.Close();
    }

    private void CreateTables()
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Displays (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Width INTEGER NOT NULL,
                Height INTEGER NOT NULL,
                Frequency INTEGER NOT NULL
            );
        ";

        command.ExecuteNonQuery();
    }

    public void AddDisplay(string deviceName, DEVMODE mode)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            INSERT INTO Displays
                (Name, Width, Height, Frequency)
            SELECT
                $name, $width, $height, $frequency
            WHERE NOT EXISTS(SELECT 1 FROM Displays WHERE Name = $name);
        ";

        command.Parameters.AddWithValue("$name", deviceName);
        command.Parameters.AddWithValue("$width", mode.dmPelsWidth);
        command.Parameters.AddWithValue("$height", mode.dmPelsHeight);
        command.Parameters.AddWithValue("$frequency", mode.dmDisplayFrequency);

        command.ExecuteNonQuery();
    }
}
