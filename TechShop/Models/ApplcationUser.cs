using Microsoft.AspNetCore.Identity;

namespace TechShop.Models
{
    internal class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
