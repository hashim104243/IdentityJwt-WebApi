using Identity___Jwt.context;
using Identity___Jwt.DTOS;
using Identity___Jwt.IdentitModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity___Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public Auth(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserRegister user)
        {
            if (user == null)
            {
                return BadRequest("user has null values");   
            }
            var appUser=new ApplicationUser
            {
                UserName=user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                
                

            };
            var result=await userManager.CreateAsync(appUser,user.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, "DefalutUser");
            var role = userManager.GetRolesAsync(appUser).Result;
                return Ok(new { message = "successfully user register ",date =appUser, UserRole=role });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserRegister user)
        {
            if (user == null)
            {
                return BadRequest("user has null");
            }
            var checkUser =await userManager.FindByEmailAsync(user.Email);
            if (checkUser==null)
            {
                return BadRequest("user must be register before login");
            }
            return Ok(new { message = "successfuly login" });
            
        }
    }
}
