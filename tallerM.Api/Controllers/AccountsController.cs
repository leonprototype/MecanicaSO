using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tallerM.Api.Helpers;
using tallerM.Shared.DTO;
using tallerM.Shared.Entities;

namespace tallerM.Api.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountsController:ControllerBase
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;
        public AccountsController(IUserHelper userHelper,IConfiguration configuration) 
        { 
            this.userHelper=userHelper;
            this.configuration=configuration;
        }
        [HttpPost("Login")]

        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await userHelper.LoginAsync(login);
            if (result.Succeeded)
            {
                var user = await userHelper.GetUserAsync(login.Email);
                return Ok(BuildToken(user));
            }
            return BadRequest("Email o contraseña incorrecta");
        }
        private object? BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]!));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiration=DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
