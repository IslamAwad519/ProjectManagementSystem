namespace ProjectManagementSystem.Api.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; } 
}
