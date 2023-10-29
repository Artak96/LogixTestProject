using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogixTestProject.Helpers
{
    public static class BerareTokem
    {
        public static string TokenGeneratorWithClaims(User user, IConfiguration _configuration)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            if (secretKey == null)
            {
                throw new Exception("JWT:Key is not defined in the configuration.");
            }

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new Exception("JWT:Issuer or JWT:Audience is not defined in the configuration.");
            }

            var tokeOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>()
                {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                },
                expires: DateTime.Now.AddHours(15),
                signingCredentials: signinCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return token;
        }
    }
}
