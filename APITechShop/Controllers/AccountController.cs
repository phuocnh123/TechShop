using Infrastructure.Accounts;
using Infrastructure.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITechShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseResult(400, GetErrorMessageFromModel(ModelState)));
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {

                return NotFound(new ResponseResult(404, "User not found"));
            }


            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {

                var roles = await _userManager.GetRolesAsync(user);
                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Email", user.Email),
                    new Claim("Id", user.Id),
                    new Claim("Roles", string.Join(",", roles)),

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

                claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claimsIdentity.Claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
               );
                var bearToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new ResponseResult(200, bearToken));
            }
            return NotFound(new ResponseResult(404, "User not found"));

        }

        private string GetErrorMessageFromModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(s => s.Errors).ToList();
            var messages = errors.Select(s => s.ErrorMessage);
            var result = string.Join(", ", messages);
            return result;
        }
    }
}
