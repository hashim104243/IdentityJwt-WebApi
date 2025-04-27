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
        public async Task seedDefaultUserAsync()
        {
            var u=await _userManager.FindByEmailAsync("Admin@gmail.com");
            if (u==null)
            {
                var user = new ApplicationUser
                {

                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    FirstName = "Hashim",
                    LastName = "khan",
                    Gender = "male"

                };
                var a=await _userManager.CreateAsync(user,"#Admin123");
                if (a.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user,"Admin");
                }
            }

        }


        

    }
}
