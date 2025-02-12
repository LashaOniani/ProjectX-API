using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _iconfiguration;

        public JwtManager(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public TokenDTO GetToken(Person user)
        {
            if (user == null || user.Id == 0)
                throw new ArgumentNullException(nameof(user), "User or User ID is invalid.");

            var tokenKey = _iconfiguration["JWT:Key"];
            if (string.IsNullOrEmpty(tokenKey))
                throw new Exception("JWT key is not configured.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(tokenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString()),
                    new Claim("RoleID", user.R_id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            try
            {
                var tokenData = tokenHandler.CreateToken(tokenDescriptor);
                var token = new TokenDTO
                {
                    AccessToken = tokenHandler.WriteToken(tokenData)
                };

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating JWT: {ex.Message}", ex);
            }
        }
    }
}
