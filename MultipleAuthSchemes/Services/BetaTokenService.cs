using Microsoft.IdentityModel.Tokens;
using MultipleAuthSchemes.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultipleAuthSchemes.Services
{
    public class BetaTokenService
    {
        public static ClientToken GenerateToken(User user)
        {
            string secret = "myunlegiveblebetasecret";
            string audience = "AudienceClientBeta";
            string issuer = "IssuerClientBeta";

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
