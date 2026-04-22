using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("createTodo")]
        public async Task<IActionResult> CreateTodo(Todo todo)
        {
            var result = await _todoService.CreateTodoAsync(todo);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("getTodo/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           return Ok(await _todoService.GetTodoByIdAsync(id));
        }

        [HttpDelete("deleteTodo/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return Ok(new { message = $"Todo with id {id} deleted successfully" });
        }

        [HttpGet("getAllTodos")]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpPut("updateTodo/{id}")]    
        public async Task<IActionResult> Update(int id, Todo todo)
        {
            var result = await _todoService.UpdateTodoAsync(id, todo);
            return Ok(result);
        }

        /*      
           [HttpPost("getTodo")]
           public IActionResult GetTodo([FromBody] GetTodoRequest request)
           {
               try
               {
                   var todoService = new TodoService();
                   if (request.Id.HasValue)
                   {
                       var todo = todoService.GetTodoById(request.Id.Value);
                       if (todo == null)
                       {
                           return NotFound();
                       }
                       return Ok(todo);
                   }
                   else
                   {
                       var todos = todoService.GetAllTodos();
                       return Ok(todos);
                   }
               }
               catch (Exception ex)
               {
                   return BadRequest(ex.Message);
               }
           }

           [HttpPost("updateTodo")]
           public IActionResult UpdateTodo([FromBody] UpdateTodoRequest request)
           {
               try
               {
                   var todoService = new TodoService();
                   var existingTodo = todoService.GetTodoById(request.Id);
                   if (existingTodo == null)
                   {
                       return NotFound();
                   }

                   var todo = new Todo
                   {
                       Title = request.Title,
                       Description = request.Description,
                       IsCompleted = request.IsCompleted
                   };

                   var result = todoService.UpdateTodo(request.Id, todo);
                   return Ok(result);
               }
               catch (Exception ex)
               {
                   return BadRequest(ex.Message);
               }
           }

           [HttpPost("deleteTodo")]
           public IActionResult DeleteTodo([FromBody] DeleteTodoRequest request)
           {
               try
               {
                   var todoService = new TodoService();
                   var result = todoService.DeleteTodo(request.Id);
                   if (result)
                   {
                       return Ok(new { message = "Todo deleted successfully" });
                   }
                   return NotFound();
               }
               catch (Exception ex)
               {
                   return BadRequest(ex.Message);
               }
           }
       }

       public class GetTodoRequest
       {
           public int? Id { get; set; }
       }

       public class UpdateTodoRequest
       {
           public int Id { get; set; }
           public string Title { get; set; }
           public string Description { get; set; }
           public bool IsCompleted { get; set; }
       }

       public class DeleteTodoRequest
       {
           public int Id { get; set; }
       }
        */
    }
}
