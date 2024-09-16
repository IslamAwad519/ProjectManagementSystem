namespace ProjectManagementSystem.Api.Models;

public class TaskItem : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    public Project Project { get; set; }
    public User User { get; set; }

}
