using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProductsManager.WebApi.Controllers
{
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "SuperSercretSuperSercretSuperSercretSuperSercret";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        [HttpGet]
        [Route("api/Token/{username}/{password}")]
        [SwaggerResponse(200, typeof(string))]
        public IActionResult Get(string username, string password )
        {
            if (username != password)
                return new ObjectResult(GenerateToken(username));
            else
                return BadRequest();
        }

        private string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, username.ToLower().Contains("admin") ? "Admin" : "Client")
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}