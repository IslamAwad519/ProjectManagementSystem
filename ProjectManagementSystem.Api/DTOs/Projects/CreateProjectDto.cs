using System.ComponentModel.DataAnnotations;
using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Projects;

public class CreateProjectDto
{
    public string Name { get; set; }
    public string Description { get; set; }
   [Range(0,1)]
    public ProjectStatus ProjectStatus { get; set; }
}
