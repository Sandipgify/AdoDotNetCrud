using AdoDotNetProj.EntityModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskEntity = AdoDotNetProj.EntityModels.Entity;

namespace AdoDotNetProj.EntityModels
{
    public static class MappingConfig
    {
        public static TaskEntity.Task ToEntity(this TaskDto taskDto)
          => new TaskEntity.Task
          {
              Name = taskDto.Name,
              Description = taskDto.Description,
              AssignTo = taskDto.AssignTo
          };

        public static TaskDto ToDto(this TaskEntity.Task taskEntity)
           => new TaskDto
           {
               Name = taskEntity.Name,
               Description = taskEntity.Description,
               AssignTo = taskEntity.AssignTo
           };
    }
}
