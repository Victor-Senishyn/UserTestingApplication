using Microsoft.AspNetCore.Identity;

namespace UserTestingApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserTestResult> ApplicationUserTests { get; set; }
    }
}