using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.Models;

public class Project : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }

    public ICollection<ProjectUser> Users { get; set; }
}
