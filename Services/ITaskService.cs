using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<TodoTask?> GetTaskByIdAsync(int id);
    Task<IEnumerable<TodoTask>> GetAllTasksAsync();
    Task<IEnumerable<TodoTask>> GetCompletedTasksAsync();
    Task<IEnumerable<TodoTask>> GetPendingTasksAsync();
    Task<TodoTask> CreateTaskAsync(string title, string? description = null);
    Task<TodoTask?> UpdateTaskAsync(int id, string? title = null, string? description = null, bool? isCompleted = null);
    Task<bool> DeleteTaskAsync(int id);
}
