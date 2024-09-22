using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.DTOs.Tasks
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Range(0, 2)]
        public Enums.TaskItemStatus TaskStatus { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
