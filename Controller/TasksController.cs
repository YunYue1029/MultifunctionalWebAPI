using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>();
        private static int nextId = 1;
        // get all tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskModel>> GetTasks()
        {
            try
            {
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
        // add a new task
        [HttpPost]
        public ActionResult<TaskModel> CreateTask([FromBody] TaskModel newTask)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newTask.Title))
                {
                    return BadRequest(new { error = "Task title cannot be empty." });
                }

                newTask.Id = nextId++;
                newTask.IsCompleted = false;
                tasks.Add(newTask);
                return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create task.", details = ex.Message });
            }
        }
        // update the status of a task
        [HttpPut("{id}")]
        public IActionResult UpdateTaskStatus(int id, [FromBody] TaskModel updatedTask)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return NotFound(new { error = "Task not found." });
                }

                task.IsCompleted = updatedTask.IsCompleted;
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to update task.", details = ex.Message });
            }
        }
    }
}