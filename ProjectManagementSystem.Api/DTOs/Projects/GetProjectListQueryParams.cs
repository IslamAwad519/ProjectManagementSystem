using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Projects;

public class GetProjectListQueryParams
{
    public string? Name { get; set; } 
    public ProjectStatus? Status { get; set; } 
    public DateTime? CreatedFrom { get; set; } 
    public DateTime? CreatedTo { get; set; }
    public string? OrderBy { get; set; }
    public bool IsDescending { get; set; } 
}
