using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SalesWebMVC.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public record LoginRequest(string UserName, string Password);
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            //Criar Token
            var token = GerarToken("juliano", "admin");
            return Ok(new { token });
        }

        private string GerarToken(string userName, string role)
        {
            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave é inválida ou nula.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Geracao do Token, enviar os Claims (Permissoes)
            var claims = new[]
            {
          new Claim(JwtRegisteredClaimNames.Sub, userName),
          new Claim(ClaimTypes.Name, userName),
          new Claim(ClaimTypes.Role, role),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), //Token expira em 1h
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}