using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Context;
using RESTfulAPI.DTO;
using RESTfulAPI.Interfaces;

namespace RESTfulAPI.Controllers
{
    public class TaskResource : ITaskResource
    {
        private readonly ApiContext _context;

        public TaskResource(ApiContext context)
        {
            _context = context;
        }

        public async Task<Models.Task?> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return null;
            }
            return task;
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return new List<Models.Task>();
            }
            var taskList = await _context.Tasks.ToListAsync();
            return taskList;
        }

        public async Task<bool> AddTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            try{
                await _context.SaveChangesAsync();
            }catch{
                return false;
            }
            return true;
        }

        public async Task<bool> ChangeTaskStatus(int id)
        {
            var task = await GetTask(id);
            if(task == null){
                return false;
            }
            task.ChangeStatus();
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditTask(int id, TaskDTO newContent)
        {
            var task = await GetTask(id);
            if(task == null){
                return false;
            }
            task.Update(newContent);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTask(int id)
        {
            var task = await GetTask(id);
            if(task == null){
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}