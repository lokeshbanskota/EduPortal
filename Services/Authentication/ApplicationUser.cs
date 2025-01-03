using Microsoft.AspNetCore.Identity;

namespace EduPortal.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string ?UserType { get; set; }
    }
}
