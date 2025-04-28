using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity___Jwt.context;
using Identity___Jwt.DTOS;
using Identity___Jwt.IdentitModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity___Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public Auth(ApplicationDbContext context,UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._context = context;
            this.userManager = userManager;
            this.configuration = configuration;
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
            var token = GenerateJwtToken(checkUser);
            if (checkUser==null)
            {
                return BadRequest("user must be register before login");
            }
            return Ok(new { message = "successfuly login", toke=token });
            
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            // Add roles if needed
            new Claim(ClaimTypes.Role, "DefalutUser"), // Assuming you want to add role as claim
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
