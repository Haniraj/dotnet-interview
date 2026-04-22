using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<Todo> GetTodoByIdAsync(int id);
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo> UpdateTodoAsync(int id, Todo todo);
        Task<bool> DeleteTodoAsync(int id);
    }
}
