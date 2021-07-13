using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetProj.EntityModels.Entity
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int? AssignTo { get; set; }
    }
}