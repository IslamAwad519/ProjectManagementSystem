namespace ProjectManagementSystem.Api.ViewModels.TaskItemVM
{
    public class TaskItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Enums.TaskItemStatus TaskStatus { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
