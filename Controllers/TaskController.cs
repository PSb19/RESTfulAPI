using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Context;
using RESTfulAPI.DTO;
using RESTfulAPI.Interfaces;

namespace RESTfulAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskResource _resource;

        public TaskController( ITaskResource resource)
        {
            _resource = resource; 
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskShortDTO>>> GetTasks()
        {
            var taskList = await _resource.GetTasks();
            if (taskList == null)
            {
                return NotFound();
            }
            var taskShortList = new List<TaskShortDTO>();
            foreach (var task in taskList){
                taskShortList.Add(new TaskShortDTO(task));
            }
            return taskShortList;
        }

        // GET: api/tasks/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            var task = await _resource.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/tasks 
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(NewTask newTask)
        {
            if(IsTaskContentEmpty(newTask.Title, newTask.Description)){
                return Problem("Task title or description is empty",
                    "Title given: "+newTask.Title+" Description given: "+newTask.Description,
                    400);
            }
            var task = new Models.Task(newTask);
            if(await _resource.AddTask(task)){
                return CreatedAtAction("GetTask", new { id = task.Id }, task);
            }
            return BadRequest();

        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, TaskDTO newContent)
        {
            if(IsTaskContentEmpty(newContent.Title, newContent.Description)){
                return Problem("New task title or description is empty",
                    "Title given: "+newContent.Title+" Description given: "+newContent.Description,
                    400);
            }
            if(await _resource.EditTask(id, newContent)){
                return NoContent();
            }
            return NotFound();
        }

        // PATCH: api/tasks/5/update_status
        [HttpPatch("{id}/update_status")]
        public async Task<IActionResult> ChangeTaskStatus(int id)
        {
            if(await _resource.ChangeTaskStatus(id)){
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if(await _resource.RemoveTask(id)){
                return NoContent();
            }
            return NotFound();
        }
        private bool IsTaskContentEmpty(string? title, string? description){
            return string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description);
        }
    }
}
