using ProjectManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.DTOs.Tasks
{
    public class UpdateTaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 2)]
        public TaskItemStatus TaskStatus { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
