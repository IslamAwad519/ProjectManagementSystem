namespace ProjectManagementSystem.Api.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } 
}
