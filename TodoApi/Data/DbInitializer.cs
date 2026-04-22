using Microsoft.Data.Sqlite;

namespace TodoApi.Data
{
    public static class DbInitializer
    {

        public static void InitializeDatabase()
        {
            var connectionString = "Data Source=todos.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Todos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Description TEXT,
            IsCompleted INTEGER NOT NULL DEFAULT 0,
            CreatedAt TEXT NOT NULL
        )";
            command.ExecuteNonQuery();

            Console.WriteLine("Database initialized successfully");
        }
    }
}
