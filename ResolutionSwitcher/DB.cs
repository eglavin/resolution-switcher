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
            CREATE TABLE IF NOT EXISTS Modes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS Displays (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ModeId INTEGER NOT NULL,
                DisplayName TEXT NOT NULL,
                Width INTEGER NOT NULL,
                Height INTEGER NOT NULL,
                Frequency INTEGER NOT NULL,
                PositionX INTEGER NOT NULL,
                PositionY INTEGER NOT NULL,
                IsPrimary INTEGER NOT NULL,

                FOREIGN KEY (ModeId)
                    REFERENCES Modes (Id)
                    ON DELETE CASCADE
                    ON UPDATE CASCADE
            );
        ";

        command.ExecuteNonQuery();
    }

    private long? GetLastInsertId()
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            SELECT last_insert_rowid();
        ";

        return (long?)command.ExecuteScalar();
    }

    public long? AddMode(string name)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            INSERT INTO Modes
                (Name)
            SELECT
                $name
            WHERE NOT EXISTS
                (SELECT 1 FROM Modes WHERE Name = $name);
        ";

        command.Parameters.AddWithValue("$name", name);

        command.ExecuteNonQuery();

        return GetLastInsertId();
    }

    public long? AddDisplay(int modeId, string displayName, DEVMODE mode, bool isPrimary)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            INSERT INTO Displays
                (ModeId, DisplayName, Width, Height, Frequency, PositionX, PositionY, IsPrimary)
            SELECT
                $modeId, $displayName, $width, $height, $frequency, $positionX, $positionY,  $isPrimary
            WHERE NOT EXISTS
                (SELECT 1 FROM Displays WHERE Name = $name)
            AND EXISTS
                (SELECT 1 FROM Modes WHERE Id = $modeId);
        ";

        command.Parameters.AddWithValue("$modeId", modeId);
        command.Parameters.AddWithValue("$displayName", displayName);
        command.Parameters.AddWithValue("$width", mode.dmPelsWidth);
        command.Parameters.AddWithValue("$height", mode.dmPelsHeight);
        command.Parameters.AddWithValue("$frequency", mode.dmDisplayFrequency);
        command.Parameters.AddWithValue("$positionX", mode.dmPositionX);
        command.Parameters.AddWithValue("$positionY", mode.dmPositionY);
        command.Parameters.AddWithValue("$isPrimary", isPrimary ? 1 : 0);

        command.ExecuteNonQuery();

        return GetLastInsertId();
    }
}
