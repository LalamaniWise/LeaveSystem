using leaveSystem.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace leaveSystem.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }

        public  ICollection<Leave> LeaveList { get; set; }
    }
}
