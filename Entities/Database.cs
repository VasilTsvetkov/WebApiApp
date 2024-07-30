using Microsoft.Data.Sqlite;

public class Database
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
        Initialize();
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS SwiftMessages (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Content TEXT
            )";
        command.ExecuteNonQuery();
    }

    public void SaveSwiftMessage(List<string> contents)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();
        foreach (var content in contents)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO SwiftMessages (Content) VALUES (@Content)";
            command.Parameters.AddWithValue("@Content", content);
            command.ExecuteNonQuery();
        }
        transaction.Commit();
    }
}