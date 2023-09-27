using RESTfulAPI.DTO;

namespace RESTfulAPI.Interfaces
{
    public interface ITaskResource
    {
        Task<Models.Task?> GetTask(int id);

        Task<IEnumerable<Models.Task>> GetTasks();

        Task<bool> AddTask(Models.Task task);

        Task<bool> EditTask(int id, TaskDTO newContent);

        Task<bool> ChangeTaskStatus(int id);

        Task<bool> RemoveTask(int id);
    }
}