using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Projects;

public class CreateProjectDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
}
