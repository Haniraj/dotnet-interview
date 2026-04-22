using Xunit;
using Moq;
using TodoApi.Controllers;
using TodoApi.Services;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Tests;

public class TodoControllerTests
{
    private readonly Mock<ITodoService> _mockService;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _mockService = new Mock<ITodoService>();
        _controller = new TodoController(_mockService.Object);
    }
    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test" };

        _mockService.Setup(s => s.CreateTodoAsync(It.IsAny<Todo>()))
                    .ReturnsAsync(todo);

        // Act
        var result = await _controller.CreateTodo(todo);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<Todo>(created.Value);

        Assert.Equal(1, value.Id);

        _mockService.Verify(s => s.CreateTodoAsync(It.IsAny<Todo>()), Times.Once);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Test1" },
            new Todo { Id = 2, Title = "Test2" }
        };

        _mockService.Setup(s => s.GetAllTodosAsync())
                    .ReturnsAsync(todos);

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<List<Todo>>(ok.Value);

        Assert.Equal(2, value.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var todo = new Todo { Id = 1, Title = "Test" };

        _mockService.Setup(s => s.GetTodoByIdAsync(1))
                    .ReturnsAsync(todo);

        var result = await _controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(todo, ok.Value);
    }

    [Fact]
    public async Task Update_ReturnsOk()
    {
        var todo = new Todo { Id = 1, Title = "Updated" };

        _mockService.Setup(s => s.UpdateTodoAsync(1, It.IsAny<Todo>()))
                    .ReturnsAsync(todo);

        var result = await _controller.Update(1, todo);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(todo, ok.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOk()
    {
        _mockService.Setup(s => s.DeleteTodoAsync(1))
                    .ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<OkObjectResult>(result);
    }
}