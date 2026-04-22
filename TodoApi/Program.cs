using Microsoft.Data.Sqlite;
using TodoApi.Data;
using TodoApi.Exceptions;
using TodoApi.Middleware;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoService>(
    _ => new TodoService("Data Source=todos.db"));
var app = builder.Build();

//DB Initialize
DbInitializer.InitializeDatabase();


//Middleware to handle exceptions globally
app.UseMiddleware<ExceptionMiddleWare>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


