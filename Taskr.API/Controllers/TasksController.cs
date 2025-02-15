using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskr.API.Data;
using Taskr.API.Models;
using Taskr.API.Services;

namespace Taskr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(TaskrDbContext context) : ControllerBase
    {
        private readonly TaskrDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TaskItemDto>>> GetTasks()
        {
            var tasksWithTags = await _context.TaskItems
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Content,
                    Tags = t.TaskTags.Select(tt => new TagDto
                    {
                        Id = tt.Tag.Id,
                        Name = tt.Tag.Name
                    }).ToList()
                })
                .ToListAsync();

            return Ok(tasksWithTags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var taskDto = await _context.TaskItems
                .Where(t => t.Id == id)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Content,
                    Status = t.Status,
                    Tags = t.TaskTags.Select(tt => new TagDto
                    {
                        Id = tt.Tag.Id,
                        Name = tt.Tag.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (taskDto is null)
                return NotFound();
            
            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> AddTask(TaskAddDto taskItem)
        {
            if (taskItem is null)
                return BadRequest();

            // split into tags
            List<string> tags = taskItem.Tags.Split(',').Select(t => t.Trim().ToLower()).Distinct().ToList();

            TaskItemService taskItemService = new TaskItemService(_context);

            var newTaskItem = await taskItemService.AddTaskItemWithTagsAsync(taskItem.Title, taskItem.Content, tags);

            return new TaskItemDto
            {
                Id = newTaskItem.Id,
                Title = newTaskItem.Title,
                Content = newTaskItem.Content,
                Tags = newTaskItem.TaskTags.Select(tt => new TagDto
                {
                    Id = tt.Tag.Id,
                    Name = tt.Tag.Name
                }).ToList()
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskEditDto updatedTask)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task is null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Content = updatedTask.Title;
            task.Status = updatedTask.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return BadRequest();

            _context.TaskItems.Remove(task);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
