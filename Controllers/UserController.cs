using ApiTest.Data;
using ApiTest.Data.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;
        public UserController(UserManager<AppUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> register([FromBody] UserDto user)
        {
            if (ModelState.IsValid) {

                AppUser u = new()
                {
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.Phone,
                };
                var res = await userManager.CreateAsync(u, user.password);

                if (res.Succeeded)
                {
                    return Ok(res);
                }

                foreach (var item in res.Errors) {
                    ModelState.AddModelError("", item.Description);
                    }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] LoginDTO user)
        {
            if (ModelState.IsValid)
            {
                AppUser? us = await userManager.FindByNameAsync(user.Name);
                if (us == null) ModelState.AddModelError("",$"there is no name : {user.Name}");
                                

                if (await userManager.CheckPasswordAsync(us, user.Password))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("RawanRas", "qad albald"));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, us.Id));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    var roles = await userManager.GetRolesAsync(us);
                    foreach (var role in roles) { 
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));
                    var sc = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: config["JWT:issuer"],
                        audience: config["JWT:audience"],
                        expires: DateTime.Now.AddHours(2),
                        signingCredentials: sc
                        );

                    var ntoken = new
                    { token = new JwtSecurityTokenHandler().WriteToken(token)
                    , expiration = token.ValidTo };   
                    return Ok(ntoken);
                    
                }
                else return Unauthorized();
                
            }
            return BadRequest(ModelState);
        }
    }
}
