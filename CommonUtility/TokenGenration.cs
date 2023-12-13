using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WellMI.CommonUtility
{
    public class TokenGenration
    {

        public string GenerateJwtToken ( int Id, string RoleId )
        {
            string key = Environment.GetEnvironmentVariable ( "JWTSecretKey" );
            int validityTimeInDays = Convert.ToInt32 ( Environment.GetEnvironmentVariable ( "ValidityTimeInDays" ) );
            var secretKey = Encoding.UTF8.GetBytes ( key );
            var signinCredentials = new SigningCredentials ( new SymmetricSecurityKey ( secretKey ), SecurityAlgorithms.HmacSha256 );
            var claims = new [] {
                new Claim("Id",Id.ToString()),
                new Claim("RoleId",ToString()),
            };
            var tokeOptions = new JwtSecurityToken (
                            //issuer: "AuthenticationTokenGeneration",
                            //audience: "AuthenticationTokenGeneration",
                            claims: claims,
                            expires: DateTime.UtcNow.AddDays ( validityTimeInDays ),
                            signingCredentials: signinCredentials
                        );
            var token = new JwtSecurityTokenHandler ().WriteToken ( tokeOptions );
            return token;
        }
    }
}
