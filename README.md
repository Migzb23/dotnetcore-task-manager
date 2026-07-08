# .NET Core In-Memory Task Manager

A simple yet complete task management API built with .NET Core 8.0, featuring dependency injection and in-memory storage.

## Features

- ✅ Create, Read, Update, Delete (CRUD) operations for tasks
- ✅ Filter tasks by completion status (completed/pending)
- ✅ In-memory storage (data persists for application lifetime)
- ✅ Dependency injection with ITaskService interface
- ✅ Comprehensive error handling and logging
- ✅ Swagger/OpenAPI documentation
- ✅ Async/await patterns throughout

## Project Structure

```
TaskManager/
├── Models/
│   └── Task.cs                  # Domain model
├── DTOs/
│   ├── CreateTaskDto.cs         # Create request DTO
│   └── UpdateTaskDto.cs         # Update request DTO
├── Services/
│   ├── ITaskService.cs          # Service interface
│   └── TaskService.cs           # In-memory implementation
├── Controllers/
│   └── TasksController.cs       # API endpoints
├── Program.cs                   # DI configuration & startup
└── appsettings.json            # Configuration
```

## Dependency Injection Setup

The application uses Microsoft's built-in dependency injection container configured in `Program.cs`:

```csharp
// Register TaskService as a singleton
builder.Services.AddSingleton<ITaskService, TaskService>();
```

### Lifetime Options Used:
- **Singleton**: `ITaskService` is registered as a singleton, ensuring the same in-memory storage instance is used throughout the application lifetime.

## API Endpoints

### Get All Tasks
```
GET /api/tasks
```
Returns all tasks.

### Get Task by ID
```
GET /api/tasks/{id}
```
Returns a specific task by ID.

### Get Completed Tasks
```
GET /api/tasks/filter/completed
```
Returns only completed tasks.

### Get Pending Tasks
```
GET /api/tasks/filter/pending
```
Returns only pending (non-completed) tasks.

### Create Task
```
POST /api/tasks
Content-Type: application/json

{
  "title": "Learn .NET Core",
  "description": "Master dependency injection and async patterns"
}
```
Creates a new task.

### Update Task
```
PUT /api/tasks/{id}
Content-Type: application/json

{
  "title": "Updated Title",
  "description": "Updated description",
  "isCompleted": true
}
```
Updates an existing task (all fields are optional).

### Delete Task
```
DELETE /api/tasks/{id}
```
Deletes a task.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application

1. **Clean and build the project:**
   ```bash
   dotnet clean
   dotnet build
   ```

2. **Run the application in Development mode:**
   ```bash
   dotnet run --environment Development
   ```
   
   Or set the environment variable explicitly:
   ```bash
   ASPNETCORE_ENVIRONMENT=Development dotnet run
   ```

3. **Access Swagger UI:**
   - **Local development**: Open `http://localhost:5000/swagger`
   - **GitHub Codespace**: The forwarded port URL will be displayed in the Ports panel
   - Once running, navigate to `/swagger/index.html`

4. **Test the API:**
   Use the Swagger UI to test all endpoints interactively, or use curl commands below.

### Example Usage with curl

```bash
# Create a task
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title":"Buy groceries","description":"Milk, eggs, bread"}'

# Get all tasks
curl http://localhost:5000/api/tasks

# Get pending tasks
curl http://localhost:5000/api/tasks/filter/pending

# Get completed tasks
curl http://localhost:5000/api/tasks/filter/completed

# Update a task (mark as completed)
curl -X PUT http://localhost:5000/api/tasks/1 \
  -H "Content-Type: application/json" \
  -d '{"isCompleted":true}'

# Get a specific task
curl http://localhost:5000/api/tasks/1

# Delete a task
curl -X DELETE http://localhost:5000/api/tasks/1
```

## Data Model

### Task
```csharp
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

## Key Design Patterns

### Dependency Injection
- `ITaskService` interface defines the contract
- `TaskService` provides the implementation
- Injected into `TasksController` via constructor
- Enables easy testing and loose coupling

### Repository Pattern
- In-memory list acts as a simple repository
- Could be easily replaced with database implementation

### DTO Pattern
- `CreateTaskDto` and `UpdateTaskDto` separate API contracts from domain models
- Provides flexibility for API evolution without domain model changes

### Async/Await
- All service methods are async
- Prepares foundation for future async operations (database calls, external APIs, etc.)

## Troubleshooting

### Getting 404 errors on Swagger?
Make sure you're running in **Development** mode:
```bash
dotnet run --environment Development
```

### HTTPS redirect issues?
In Development mode, HTTPS redirects are disabled. Use `http://` URLs:
```
http://localhost:5000/swagger
```

## Future Enhancements

- [ ] Add database persistence (SQL Server, PostgreSQL, etc.)
- [ ] Implement unit tests
- [ ] Add authentication and authorization
- [ ] Add task priority and due dates
- [ ] Implement pagination and sorting
- [ ] Add task categories/tags
- [ ] Implement caching
- [ ] Add background jobs for task cleanup

## License

MIT License - feel free to use this project as a learning resource or starting template.
