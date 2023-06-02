using Microsoft.Data.Sqlite;

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
}
