using Microsoft.Data.Sqlite;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private string _connectionString;

        // private string _connectionString = "Data Source=todos.db";

        public TodoService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

       
            var cmd = connection.CreateCommand();
            cmd.CommandText= @"
                INSERT INTO Todos (Title, Description, IsCompleted, CreatedAt)
                VALUES (@title, @description, @isCompleted, @createdAt);
                SELECT last_insert_rowid();
            ";

            cmd.Parameters.AddWithValue("@title", todo.Title);
            cmd.Parameters.AddWithValue("@description", todo.Description);
            cmd.Parameters.AddWithValue("@isCompleted", todo.IsCompleted ? 1 : 0);
            cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow.ToString("o"));

            todo.Id = (int)(long)await cmd.ExecuteScalarAsync();
            todo.CreatedAt = DateTime.UtcNow;
            return todo;
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            var list = new List<Todo>();

            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Todos WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapReaderToTodo(reader);
            throw new NotFoundException($"Todo with id {id} not found");
        }

      
        public async Task<bool> DeleteTodoAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Todos WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
                throw new NotFoundException($"Todo with id {id} not found");
            return true;
        }

        public Task<List<Todo>> GetAllTodosAsync()
        {
            throw new NotImplementedException();
        }

       
        public Task<Todo> UpdateTodoAsync(int id, Todo todo)
        {
            throw new NotImplementedException();
        }

        private Todo MapReaderToTodo(SqliteDataReader reader)
        {
            return new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                IsCompleted = reader.GetInt32(3) == 1,
                CreatedAt = DateTime.Parse(reader.GetString(4))
            };
        }

        /*
        public Todo CreateTodo(Todo todo)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $@"
                INSERT INTO Todos (Title, Description, IsCompleted, CreatedAt)
                VALUES ('{todo.Title}', '{todo.Description}', {(todo.IsCompleted ? 1 : 0)}, '{DateTime.UtcNow.ToString("o")}');
                SELECT last_insert_rowid();
            ";

            var id = Convert.ToInt32(command.ExecuteScalar());
            todo.Id = id;
            todo.CreatedAt = DateTime.UtcNow;
            return todo;
        }

        public List<Todo> GetAllTodos()
        {
            var todos = new List<Todo>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Todos";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                todos.Add(new Todo
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    IsCompleted = reader.GetInt32(3) == 1,
                    CreatedAt = DateTime.Parse(reader.GetString(4))
                });
            }

            return todos;
        }

        public Todo GetTodoById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Todos WHERE Id = {id}";

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Todo
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    IsCompleted = reader.GetInt32(3) == 1,
                    CreatedAt = DateTime.Parse(reader.GetString(4))
                };
            }

            return null;
        }

        public Todo UpdateTodo(int id, Todo todo)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $@"
                UPDATE Todos
                SET Title = '{todo.Title}', Description = '{todo.Description}', IsCompleted = {(todo.IsCompleted ? 1 : 0)}
                WHERE Id = {id}
            ";

            var rowsAffected = command.ExecuteNonQuery();

            todo.Id = id;
            return todo;
        }

        public bool DeleteTodo(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Todos WHERE Id = {id}";

            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        */

    }
}
