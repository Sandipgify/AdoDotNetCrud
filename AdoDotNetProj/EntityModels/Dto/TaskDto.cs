using System.ComponentModel;

namespace AdoDotNetProj.EntityModels.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        [DisplayName("Task Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        [DisplayName("Created By")]
        public string CretedByName { get; set; }
        [DisplayName("Assigned To")]
        public int? AssignTo { get; set; }
        [DisplayName("Assigned To")]
        public string AssignName { get; set; }

    }
}
