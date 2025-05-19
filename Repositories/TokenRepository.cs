using Microsoft.AspNetCore.Identity;




using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
namespace nzWalksApi.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public IConfiguration Configuration { get; set; }

        public TokenRepository(IConfiguration configuration) {
        this.Configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // create claimes 
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
