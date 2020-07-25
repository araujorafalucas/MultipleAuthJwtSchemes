using CursoApiCore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultipleAuthSchemes.Services
{
    public class BetaTokenService
    {
        public static ClientToken GenerateToken(User user)
        {
            string secret = "myunlegiveblealphasecret";
            string audience = "AudienceClientAlpha";
            string issuer = "IssuerClientAlpha";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expiration = DateTime.UtcNow.AddHours(2);
            var claims = new[]{
                                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                };

            JwtSecurityToken token = new JwtSecurityToken(
                                                           audience: audience
                                                          , issuer: issuer
                                                          , claims: claims
                                                          , expires: expiration
                                                          , signingCredentials: credenciais);

            ClientToken clientToken = new ClientToken();
            clientToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            clientToken.DateExpiration = expiration;
            return clientToken;
        }
    }
}
