namespace ProjectManagementSystem.Api.DTOs.Tasks
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> ProjectIds { get; set; }
        public List<int> AssignedUserIds { get; set; }


      
        
    }
}
