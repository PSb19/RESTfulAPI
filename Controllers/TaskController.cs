using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Context;
using RESTfulAPI.DTO;

namespace RESTfulAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApiContext _context;

        public TaskController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/tasks DONE
        //Endpoint that returns list of all tasks with statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskShortDTO>>> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var taskList = await _context.Tasks.ToListAsync();
            var taskShortList = new List<TaskShortDTO>();
            foreach (var task in taskList){
                taskShortList.Add(new TaskShortDTO(task));
            }
            return taskShortList;
        }

        // GET: api/tasks/5 DONE
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/tasks DONE?
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(NewTask newTask)
        {
            if(IsTaskContentEmpty(newTask.Title, newTask.Description)){
                return Problem("Task title or description is empty",
                    "Title given: "+newTask.Title+" Description given: "+newTask.Description,
                    400);
            }
            var task = new Models.Task(newTask);
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // PUT: api/tasks/5 DONE?
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, TaskDTO newContent)
        {
            if(IsTaskContentEmpty(newContent.Title, newContent.Description)){
                return Problem("New task title or description is empty",
                    "Title given: "+newContent.Title+" Description given: "+newContent.Description,
                    400);
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.update(newContent);
            _context.Entry(task).State = EntityState.Modified;

            await _context.SaveChangesAsync(); //*
            return NoContent();
        }

        // PATCH: api/tasks/5/update_status DONE?
        [HttpPatch("{id}/update_status")]
        public async Task<IActionResult> ChangeTaskStatus(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.changeStatus();
            _context.Entry(task).State = EntityState.Modified;

            await _context.SaveChangesAsync(); //*
            return NoContent();
        }

        // DELETE: api/tasks/5 DONE?
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool IsTaskContentEmpty(string? title, string? description){
            return string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description);

        }
    }
}
