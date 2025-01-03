using Microsoft.AspNetCore.Identity;

namespace Services.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string ?UserType { get; set; }
    }
}
