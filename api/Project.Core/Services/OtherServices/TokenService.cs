using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Entities;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.OtherServices
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey key;

         public TokenService(IConfiguration config)
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenhandler.WriteToken(token));
        }
    }
}