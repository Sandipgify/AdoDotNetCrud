using AdoDotNetProj.EntityModels.Dto;
using AdoDotNetProj.Repository.Interface;
using Database.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityTask = AdoDotNetProj.EntityModels.Entity;

namespace AdoDotNetProj.Repository
{
    public class TaskRepository:ITaskRepository
    {
        private readonly IDbAccessManager _dbAccessManager;

        public TaskRepository(IDbAccessManager dbAccessManager)
        {
            _dbAccessManager = dbAccessManager;
        }

        public async Task<EntityTask.Task> GetTaskAsync(int Id)
        {
            var query = "select * from Task where id=@id";
            var task = (await _dbAccessManager.ReadDataAsync<EntityTask.Task>(query, new { id = Id })).FirstOrDefault();
            return task;
        }

        public async Task<IEnumerable<TaskDto>> GetTaskAsync()
        {
            var query = @"select T.Id,Name,Description,u.FirstName+' '+u.LastName as CretedByName,U2.FirstName+' '+U2.LastName as AssignName from Task as T inner join users as u on T.CreatedBy=u.id Left join users as U2 on T.AssignTo = u2.id";
            var task = await _dbAccessManager.ReadDataAsync<TaskDto>(query);
            return task;
        }

        public async Task<IEnumerable<TaskDto>> GetAssignedTaskAsync(int assignTo)
        {
            var query = "select Name,Description,u.FirstName+' '+u.LastName as CretedByName,U2.FirstName+' '+U2.LastName as AssignName from Task as T inner join users as u on T.CreatedBy=u.id Left join users as U2 on T.AssignTo = u2.id where (AssignTo=@assign or @assign=0)";
            var task = await _dbAccessManager.ReadDataAsync<TaskDto>(query, new { assign = assignTo });
            return task;
        }

        public async Task<int> CreateAsync(EntityTask.Task task)
        {
            var query = @"INSERT INTO [dbo].[Task]
           ([Name]
           ,[Description]
           ,[CreatedBy]
           ,[AssignTo]
           )
     VALUES
           (@Name
           ,@description
           ,@createdBy
           ,@assignTo
          ) select scope_identity()";
            var insertedRowId = await _dbAccessManager.GetScalarAsync<int>(query, new { Name = task.Name, description = task.Description, createdBy = task.CreatedBy, assignTo = task.AssignTo});
            return (int)insertedRowId;
        }

        public async Task<int> UpdateAsync(EntityTask.Task task)
        {
            var query = "Update Task set Name=@name,Description=@desc where id=@id";
            var rowAffected = await _dbAccessManager.UpdateAsync(query, new { name = task.Name, desc = task.Description, id = task.Id });
            return rowAffected;
        }

        public async Task<int> AssignTask(EntityTask.Task task)
        {
            var query = "Update Task set AssignTo=@assign where id=@id";
            var rowAffected = await _dbAccessManager.UpdateAsync(query, new { assign = task.AssignTo, id = task.Id });
            return rowAffected;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "delete from task where id=@id";
            var rowAffected = await _dbAccessManager.DeleteAsync(query, new { id = id });
            return rowAffected;
        }
    }
}
