using Microsoft.AspNetCore.Identity;

namespace Identity2.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}