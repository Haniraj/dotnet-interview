using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<Todo> CreateTodoAsync(Todo todo);
    }
}
