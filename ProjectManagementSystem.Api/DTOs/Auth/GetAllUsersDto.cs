using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.DTOs.Auth
{
    public class GetAllUsersDto
    {
        public string UserName { get; set; }
        public UserStatus? Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set;}
    }
}
