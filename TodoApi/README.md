# TODO API

A simple TODO API built with ASP.NET Core 8.0.

## Getting Started

### Prerequisites
- .NET 8.0 SDK

### Install Required packages
- dotnet add package Microsoft.EntityFrameworkCore 
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite
- dotnet add package Microsoft.EntityFrameworkCore.Tools

## Database
The application uses EF Core
### Run Migration
- Add-Migration InitialCreate
- Update-Database

### Running the Application

1. Navigate to the TodoApi directory:
```
cd TodoApi
```

2. Run the application:
```
dotnet run
```

3. The API will be available at `http://localhost:5164` (or check the console output for the exact URL)

4. Access Swagger UI at `http://localhost:5164/swagger` to test the endpoints

## API Endpoints

All endpoints are under `/api`:

/api/Todo/createTodo

- `POST  /api/Todo/createTodo` - Create a new TODO item
- `GET   /api/Todo/getAllTodos` - Get All TODO item(s)
- `GET   /api/Todo/getTodo/{id}` - Get TODO item of particular id
- `DELETE /api/Todo/deleteTodo/{id}` - Delete a TODO item
- `PUT  /api/Todo/updateTodo/{id}` - Update a TODO item

## Testing

Run the tests with:
```
cd TodoApi.Tests
dotnet test
```

