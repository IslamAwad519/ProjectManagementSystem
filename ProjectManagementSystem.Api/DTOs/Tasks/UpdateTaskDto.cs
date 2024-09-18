using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Tasks
{
    public class UpdateTaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public int UserId { get; set; }
    }
}
