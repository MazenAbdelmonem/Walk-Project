﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalk.API.Repositories
{
    public class TokenRepositry : ITokenReositry
    {
        private readonly IConfiguration configuration;

        public TokenRepositry(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreatJwtToken(IdentityUser user, List<string> roles)
        {
            // Creat Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials : credential);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
