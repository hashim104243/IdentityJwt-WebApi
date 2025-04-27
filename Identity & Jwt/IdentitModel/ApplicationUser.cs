using Microsoft.AspNetCore.Identity;

namespace Identity___Jwt.IdentitModel
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Gender { get; set; }

        
    }
}
