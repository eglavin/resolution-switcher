using Microsoft.Data.Sqlite;
using static ResolutionSwitcher.Structs;

namespace ResolutionSwitcher;
public class DB
{
    private readonly SqliteConnection _connection;

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

        public Display(int id, int modeId, string displayName, int width, int height, int frequency, int positionX, int positionY, bool isPrimary)
        {
            Id = id;
            ModeId = modeId;
            DisplayName = displayName;
            Width = width;
            Height = height;
            Frequency = frequency;
            PositionX = positionX;
            PositionY = positionY;
            IsPrimary = isPrimary;
        }
    }

    public class Mode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Display> Displays { get; set; }

        public Mode(int id, string name)
        {
            Id = id;
            Name = name;
            Displays = new List<Display>();
        }
    }

    public List<Mode> GetDisplayModes()
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            SELECT
                Modes.Id, Modes.Name, Displays.Id, Displays.DisplayName, Displays.Width, Displays.Height, Displays.Frequency, Displays.PositionX, Displays.PositionY, Displays.IsPrimary
            FROM
                Modes
            INNER JOIN Displays
            ON Modes.Id = Displays.ModeId
            ORDER BY Modes.Id, Displays.Id;
        ";

        var displayModes = new List<Mode>();

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var modeId = reader.GetInt32(0);
            var modeName = reader.GetString(1);
            var displayId = reader.GetInt32(2);
            var displayName = reader.GetString(3);
            var width = reader.GetInt32(4);
            var height = reader.GetInt32(5);
            var frequency = reader.GetInt32(6);
            var positionX = reader.GetInt32(7);
            var positionY = reader.GetInt32(8);
            var isPrimary = reader.GetInt32(9) == 1;


            var displayModeIndex = displayModes.FindIndex((dm) => dm.Id == modeId);

            if (displayModeIndex == -1)
            {
                var mode = new Mode(modeId, modeName);
                mode.Displays.Add(new Display(displayId, modeId, displayName, width, height, frequency, positionX, positionY, isPrimary));
                displayModes.Add(mode);
            }
            else
            {
                displayModes[displayModeIndex].Displays.Add(new Display(displayId, modeId, displayName, width, height, frequency, positionX, positionY, isPrimary));
            }
        }

        return displayModes;
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


    public void UpdateMode(int modeId, string name)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            UPDATE Modes
            SET
                Name = $name
            WHERE
                Id = $modeId;
        ";

        command.Parameters.AddWithValue("$modeId", modeId);
        command.Parameters.AddWithValue("$name", name);

        command.ExecuteNonQuery();
    }

    public void UpdateDisplay(int displayId, int modeId, string displayName, DEVMODE mode, bool isPrimary)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            UPDATE Displays
            SET
                ModeId = $modeId,
                DisplayName = $displayName,
                Width = $width,
                Height = $height,
                Frequency = $frequency,
                PositionX = $positionX,
                PositionY = $positionY,
                IsPrimary = $isPrimary
            WHERE
                Id = $displayId;
        ";

        command.Parameters.AddWithValue("$displayId", displayId);
        command.Parameters.AddWithValue("$modeId", modeId);
        command.Parameters.AddWithValue("$displayName", displayName);
        command.Parameters.AddWithValue("$width", mode.dmPelsWidth);
        command.Parameters.AddWithValue("$height", mode.dmPelsHeight);
        command.Parameters.AddWithValue("$frequency", mode.dmDisplayFrequency);
        command.Parameters.AddWithValue("$positionX", mode.dmPositionX);
        command.Parameters.AddWithValue("$positionY", mode.dmPositionY);
        command.Parameters.AddWithValue("$isPrimary", isPrimary ? 1 : 0);

        command.ExecuteNonQuery();
    }

    public void DeleteMode(int modeId)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            DELETE FROM Modes
            WHERE Id = $modeId;
        ";

        command.Parameters.AddWithValue("$modeId", modeId);

        command.ExecuteNonQuery();
    }

    public void DeleteDisplay(int displayId)
    {
        var command = _connection.CreateCommand();

        command.CommandText = @"
            DELETE FROM Displays
            WHERE Id = $displayId;
        ";

        command.Parameters.AddWithValue("$displayId", displayId);

        command.ExecuteNonQuery();
    }
}
