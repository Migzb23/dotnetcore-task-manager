using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly List<TodoTask> _tasks = new();
    private int _nextId = 1;

    public Task<TodoTask?> GetTaskByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult<TodoTask?>(task);
    }

    public Task<IEnumerable<TodoTask>> GetAllTasksAsync()
    {
        return Task.FromResult<IEnumerable<TodoTask>>(_tasks.ToList());
    }

    public Task<IEnumerable<TodoTask>> GetCompletedTasksAsync()
    {
        var completedTasks = _tasks.Where(t => t.IsCompleted).ToList();
        return Task.FromResult<IEnumerable<TodoTask>>(completedTasks);
    }

    public Task<IEnumerable<TodoTask>> GetPendingTasksAsync()
    {
        var pendingTasks = _tasks.Where(t => !t.IsCompleted).ToList();
        return Task.FromResult<IEnumerable<TodoTask>>(pendingTasks);
    }

    public Task<TodoTask> CreateTaskAsync(string title, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty.", nameof(title));
        }

        var task = new TodoTask
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task<TodoTask?> UpdateTaskAsync(int id, string? title = null, string? description = null, bool? isCompleted = null)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return Task.FromResult<TodoTask?>(null);
        }

        if (!string.IsNullOrWhiteSpace(title))
        {
            task.Title = title;
        }

        if (description != null)
        {
            task.Description = description;
        }

        if (isCompleted.HasValue)
        {
            task.IsCompleted = isCompleted.Value;
        }

        task.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult<TodoTask?>(task);
    }

    public Task<bool> DeleteTaskAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return Task.FromResult(false);
        }

        _tasks.Remove(task);
        return Task.FromResult(true);
    }
}
