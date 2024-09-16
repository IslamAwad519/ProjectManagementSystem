namespace ProjectManagementSystem.Api.Models;

public class ProjectUser : BaseModel
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    public Project Project { get; set; }
    public User User { get; set; }
}
