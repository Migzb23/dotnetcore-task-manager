using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<Task?> GetTaskByIdAsync(int id);
    Task<IEnumerable<Task>> GetAllTasksAsync();
    Task<IEnumerable<Task>> GetCompletedTasksAsync();
    Task<IEnumerable<Task>> GetPendingTasksAsync();
    Task<Task> CreateTaskAsync(string title, string? description = null);
    Task<Task?> UpdateTaskAsync(int id, string? title = null, string? description = null, bool? isCompleted = null);
    Task<bool> DeleteTaskAsync(int id);
}