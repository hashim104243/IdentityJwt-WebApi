using Identity___Jwt.IdentitModel;
using Microsoft.AspNetCore.Identity;

namespace Identity___Jwt.Seed
{
    public class DefaultRoles
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public DefaultRoles(RoleManager<ApplicationRole>  roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task SeedDefaultRolesAsync()
        {

            var role = new[] { "Admin", "basicUser", "defaultUser" };
            foreach (var item in role)
            {
                var roleExist =await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = item });
                }
            }
        }

    }
}
