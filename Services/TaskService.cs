using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly List<Task> _tasks = new();
    private int _nextId = 1;

    public Task<Task?> GetTaskByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult<Task?>(task);
    }

    public Task<IEnumerable<Task>> GetAllTasksAsync()
    {
        return Task.FromResult<IEnumerable<Task>>(_tasks.ToList());
    }

    public Task<IEnumerable<Task>> GetCompletedTasksAsync()
    {
        var completedTasks = _tasks.Where(t => t.IsCompleted).ToList();
        return Task.FromResult<IEnumerable<Task>>(completedTasks);
    }

    public Task<IEnumerable<Task>> GetPendingTasksAsync()
    {
        var pendingTasks = _tasks.Where(t => !t.IsCompleted).ToList();
        return Task.FromResult<IEnumerable<Task>>(pendingTasks);
    }

    public Task<Task> CreateTaskAsync(string title, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty.", nameof(title));
        }

        var task = new Task
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

    public Task<Task?> UpdateTaskAsync(int id, string? title = null, string? description = null, bool? isCompleted = null)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return Task.FromResult<Task?>(null);
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

        return Task.FromResult<Task?>(task);
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