using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class TokenService
    {
        public string GenerateToken(IdentityUser user)
        {
            var key = Encoding.ASCII.GetBytes
                  ("MojaAmavi020793-OFFKDI40NG7:5675253-tyuw-5769-021-kfirox29zoxv");
            //Generate Token for user 
            var JWToken = new JwtSecurityToken(
                issuer: "https://localhost:44352",
                audience: "https://localhost:44352",
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }

        private IEnumerable<Claim> GetUserClaims(IdentityUser user)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.NormalizedUserName),
                new Claim("USERID", user.Id),
                new Claim("ACCESS_LEVEL", "USER"),
                new Claim("READ_ONLY", "READONLY")
            };
            return claims;
        }
    }
}
