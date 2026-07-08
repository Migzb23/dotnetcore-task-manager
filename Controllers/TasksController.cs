using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        try
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tasks");
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        try
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving task");
            return StatusCode(500, "An error occurred while retrieving the task");
        }
    }

    [HttpGet("filter/completed")]
    public async Task<IActionResult> GetCompletedTasks()
    {
        try
        {
            var tasks = await _taskService.GetCompletedTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving completed tasks");
            return StatusCode(500, "An error occurred while retrieving completed tasks");
        }
    }

    [HttpGet("filter/pending")]
    public async Task<IActionResult> GetPendingTasks()
    {
        try
        {
            var tasks = await _taskService.GetPendingTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending tasks");
            return StatusCode(500, "An error occurred while retrieving pending tasks");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(createTaskDto.Title))
            {
                return BadRequest("Task title is required");
            }

            var task = await _taskService.CreateTaskAsync(createTaskDto.Title, createTaskDto.Description);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid task data");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task");
            return StatusCode(500, "An error occurred while creating the task");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            var task = await _taskService.UpdateTaskAsync(id, updateTaskDto.Title, updateTaskDto.Description, updateTaskDto.IsCompleted);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task");
            return StatusCode(500, "An error occurred while updating the task");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task");
            return StatusCode(500, "An error occurred while deleting the task");
        }
    }
}
