using Identity___Jwt.IdentitModel;
using Microsoft.AspNetCore.Identity;

namespace Identity___Jwt.Seed
{
    public class DefaultRoles
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DefaultRoles(RoleManager<ApplicationRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task SeedDefalutRolesAsync()
        {
            var roles = new[] { "superAdmin", "Admin", "DefalutUser" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                }

            }
        }
    }
}

