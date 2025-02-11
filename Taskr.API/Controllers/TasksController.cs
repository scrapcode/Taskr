using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskr.API.Data;
using Taskr.API.Models;

namespace Taskr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(TaskrDbContext context) : ControllerBase
    {
        private readonly TaskrDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetTasks()
        {
            return Ok(await _context.TaskItems.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task is null)
                return NotFound();
            
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> AddTask(TaskItem taskItem)
        {
            if (taskItem is null)
                return BadRequest();

            _context.TaskItems.Add(taskItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem updatedTask)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task is null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Content = updatedTask.Title;

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
