using ProjectManagementSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.Dtos.Tasks
{
    public class GetTaskListQueryParams
    {
        public string? Name { get; set; }         // Search by task name
        [Range(0, 2)]
        public TaskItemStatus? Status { get; set; }   // Filter by task status
        public DateTime? CreatedFrom { get; set; } // Filter tasks created after this date
        public DateTime? CreatedTo { get; set; }   // Filter tasks created before this date
        public string? OrderBy { get; set; }      // Order the result by a field (e.g., name, creation time)
        public bool IsDescending { get; set; }    // Order direction: ascending or descending

     
    }
}
