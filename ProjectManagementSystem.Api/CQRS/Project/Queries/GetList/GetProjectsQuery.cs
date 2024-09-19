using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;

public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
    public string? Name { get; set; } // For searching by name
    public ProjectStatus? Status { get; set; } 
    public DateTime? CreatedFrom { get; set; } // Start date 
    public DateTime? CreatedTo { get; set; }   // End date 
    public string? OrderBy { get; set; } // Name, CreationTime
    public bool IsDescending { get; set; } = false; 

    public GetProjectsQuery(
        bool isDescending,
        string? name = null, 
        ProjectStatus? status = null, 
        DateTime? createdFrom = null, 
        DateTime? createdTo = null,
        string? orderBy = null
         )
    {
        Name = name;
        Status = status;
        CreatedFrom = createdFrom;
        CreatedTo = createdTo;
        OrderBy = orderBy;
        IsDescending = isDescending;
    }
}