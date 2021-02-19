using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Core
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly AppSettings _settings;

        public JwtTokenGenerator(AppSettings settings)
        {
            _settings = settings;
        }
        public async Task<JwtSecurityToken> GenerateToken(UserDto user)
        {
            return await Task.Run(() =>
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Authorization.SecretKey));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.Authorization.MinutesToExpiration),
                signingCredentials: credentials
                );

                return token;
            });
        }
    }
}
