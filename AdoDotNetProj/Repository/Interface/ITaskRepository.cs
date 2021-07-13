using AdoDotNetProj.EntityModels.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityTask = AdoDotNetProj.EntityModels.Entity;

namespace AdoDotNetProj.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<EntityTask.Task> GetTaskAsync(int Id);
        Task<IEnumerable<TaskDto>> GetTaskAsync();
        Task<IEnumerable<TaskDto>> GetAssignedTaskAsync(int assignTo);
        Task<int> CreateAsync(EntityTask.Task task);
        Task<int> UpdateAsync(EntityTask.Task task);
        Task<int> AssignTask(EntityTask.Task task);
        Task<int> DeleteAsync(int id);

    }
}