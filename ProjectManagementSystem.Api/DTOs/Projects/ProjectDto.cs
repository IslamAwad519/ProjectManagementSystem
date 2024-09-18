using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Projects;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    public string CreationTime { get; set; }

    //public ICollection<int> UserIds { get; set; }  // Assuming you pass user IDs to associate with the project
}
