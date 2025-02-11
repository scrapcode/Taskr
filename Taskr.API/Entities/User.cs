using Microsoft.AspNetCore.Identity;

namespace Taskr.API.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
    }
}
