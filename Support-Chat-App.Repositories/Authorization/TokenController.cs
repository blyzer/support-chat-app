using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Support_Chat_App.Data.Helpers;
using Support_Chat_App.Data.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Support_Chat_App.Repositories.Authorization
{
    public class TokenController : ITokenController
    {
        private readonly AppSettings _appSettings;

        public TokenController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// This is for generate Jwt Token
        /// Lifespan - 7 days
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        public string GenerateToken(UserDto user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var securityTokenDescriptor = GenerateSecurityTokenDescriptor(user.Id);
            var token = jwtTokenHandler.CreateToken(securityTokenDescriptor);
            
            return jwtTokenHandler.WriteToken(token);
        }

        /// <summary>
        /// To generate security token descriptor
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Security Token Descriptor</returns>
        private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(long userId)
        {
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.Secret);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        /// <summary>
        /// Validate JWT token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Returns User Id if the token is valid</returns>
        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
