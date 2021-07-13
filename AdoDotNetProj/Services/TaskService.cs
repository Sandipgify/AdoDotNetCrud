using AdoDotNetProj.EntityModels;
using AdoDotNetProj.EntityModels.Dto;
using AdoDotNetProj.Repository.Interface;
using AdoDotNetProj.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdoDotNetProj.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync()

            => await _taskRepository.GetTaskAsync();



        public async Task<TaskDto> GetByIdAsync(int Id)
          => (await _taskRepository.GetTaskAsync(Id)).ToDto();

        public async Task<IEnumerable<TaskDto>> GetAssignedTaskAsync(int AssignedTo)
          => await _taskRepository.GetAssignedTaskAsync(AssignedTo);


        public async Task<int> SaveAsync(TaskDto taskDto)
        {
            var taskEntity = taskDto.ToEntity();
            taskEntity.CreatedBy = taskDto.CreatedBy;
            var id = await _taskRepository.CreateAsync(taskEntity);
            return id;
        }

        public async Task<TaskDto> UpdateAsync(int Id, TaskDto taskDto)
        {

            var existingTask = await _taskRepository.GetTaskAsync(Id);
            if (existingTask == null)
                return null;
            existingTask.Name = taskDto.Name;
            existingTask.Description = taskDto.Description;

            await _taskRepository.UpdateAsync(existingTask);
            return existingTask.ToDto();
        }

        public async Task<bool> AsignTaskAsync(int Id, TaskDto taskDto)
        {
            var existingTask = await _taskRepository.GetTaskAsync(Id);
            if (existingTask == null)
                return false;
            existingTask.AssignTo = taskDto.AssignTo;
            await _taskRepository.AssignTask(existingTask);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int Id)
        {
            var existingTask = await _taskRepository.GetTaskAsync(Id);
            if (existingTask == null)
                return false;
            await _taskRepository.DeleteAsync(Id);
            return true;
        }

    }

}
