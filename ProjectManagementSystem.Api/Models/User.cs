using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.Models;

public class User : BaseModel
{
    public string UserName { get; set; }
    public ICollection<ProjectUser> Projects { get; set; }

}
