using System.IdentityModel.Tokens.Jwt; // Contains the JwtSecurityToken class that is used to generate the JWT token.
using System.Security.Claims; // Contains classes that represent claims (such as role and email).
using System.Text; // Required for encoding the secret key.
using Microsoft.AspNetCore.Identity; // Required to work with the IdentityUser class.
using Microsoft.Extensions.Configuration; // Required for accessing configuration settings like the JWT key.
using Microsoft.IdentityModel.Tokens; // Required for creating JWT tokens and security-related operations.

namespace nzWalksApi.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public IConfiguration Configuration { get; set; } // Configuration object to access application settings.

        // Constructor: Injects the IConfiguration to access the configuration settings.
        public TokenRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method creates a JWT token for an authenticated user, including claims for roles and email.
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Claims: Represent information about the user. Claims are key-value pairs that hold information about the user.
            var claims = new List<Claim>
            {
                // Adding an email claim to the token.
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Adding role claims to the token. A user can have multiple roles.
            foreach (var role in roles)
            {
                // Adding role as a claim for the user.
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // JWT Key: Secret key used for signing the token. This key should be kept secure.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            // Signing Credentials: The credentials used to sign the JWT token, which includes the signing algorithm (HmacSha256).
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Creating the JWT token using the JwtSecurityToken class.
            var token = new JwtSecurityToken(
                // Issuer: The entity that created the token (usually the authentication server).
                issuer: Configuration["Jwt:Issuer"],
                // Audience: The intended recipient of the token (usually the API the client is accessing).
                audience: Configuration["Jwt:Audience"],
                // Claims: Adding the claims to the token (such as user email and roles).
                claims: claims,
                // Expiration: Setting the token's expiration time. After this time, the token will no longer be valid.
                expires: DateTime.Now.AddMinutes(15), // Token expires after 15 minutes.
                // Signing Credentials: The credentials used to sign the token.
                signingCredentials: credentials
            );

            // Returning the JWT token as a string.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
