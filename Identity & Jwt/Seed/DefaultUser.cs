using Identity___Jwt.IdentitModel;
using Microsoft.AspNetCore.Identity;

namespace Identity___Jwt.Seed
{
    public class DefaultUser
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DefaultUser(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task SeedDefaultUserAsync()
        {
            var existUser = await _userManager.FindByEmailAsync("Admin@gmail.com");
            if (existUser == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Hashim",
                    LastName = "khan",
                    UserName = "Admin@gmail.com",
                    Email = "Admin@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, "@Admin123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                }
            }
        }
    }
}
