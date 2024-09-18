namespace ProjectManagementSystem.Api.Models;

public class TaskItem : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Enums.TaskItemStatus TaskStatus { get; set; }
    public int ProjectId { get; set; }  //FK
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Project Project { get; set; }  //NP
    public User User { get; set; }

}
