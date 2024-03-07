using Microsoft.AspNetCore.Identity;

namespace UserTestingApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Test> Tests { get; set; }
    }
}
