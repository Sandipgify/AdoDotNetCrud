using AdoDotNetProj.EntityModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetProj.Services.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync();
        Task<TaskDto> GetByIdAsync(int Id);
        Task<int> SaveAsync(TaskDto taskDto);
        Task<TaskDto> UpdateAsync(int Id, TaskDto taskDto);
        Task<bool> AsignTaskAsync(int Id, TaskDto taskDto);
        Task<bool> DeleteTaskAsync(int Id);
        Task<IEnumerable<TaskDto>> GetAssignedTaskAsync(int AssignedTo);
    }
}
