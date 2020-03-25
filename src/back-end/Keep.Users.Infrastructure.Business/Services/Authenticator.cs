using Keep.Users.Services.Abstractions;
using Keep.Users.Services.Abstractions.Dtos;
using Keep.Users.Services.Abstractions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Keep.Users.Infrastructure.Business.Services
{
    public class Authenticator : IAuthenticator
    {
        #region Private fields
        private readonly ILogger<Authenticator> _logger;
        private readonly IOptions<AuthOptions> _authOptions;
        #endregion

        public Authenticator(ILogger<Authenticator> logger, IOptions<AuthOptions> authOptions)
        {
            _logger = logger;
            _authOptions = authOptions;
        }

        public UserContext AuthByCredentials(AuthByCredentialsDto dto)
        {
            if (dto.Login != "root" || dto.Password != "root")
                throw new UnauthorizedAccessException();

            JwtSecurityToken jwt = GenerateToken(dto);
            return new UserContext
            {
                AccessToken = new JwtSecurityTokenHandler()
                                    .WriteToken(jwt),
                ExpiresIn = (int)TimeSpan
                                    .FromMinutes(_authOptions.Value.Lifetime)
                                    .TotalSeconds
            };
        }

        #region Private methods
        private JwtSecurityToken GenerateToken(AuthByCredentialsDto dto)
        {
            var authTime = DateTime.UtcNow;
            var claims = GenerateClaims(dto, authTime);

            var epxiresInTime = authTime.Add(
                TimeSpan.FromMinutes(_authOptions.Value.Lifetime)
            );

            var credentials = new SigningCredentials(
                _authOptions.Value.GetSecurityKey(),
                SecurityAlgorithms.HmacSha256
            );

            return new JwtSecurityToken(
                            issuer: _authOptions.Value.Issuer,
                            audience: _authOptions.Value.Audience,
                            claims: claims,
                            notBefore: authTime,
                            expires: epxiresInTime,
                            signingCredentials: credentials
            );
        }

        private static Claim[] GenerateClaims(AuthByCredentialsDto dto, DateTime tokenAuthTime)
        {
            return new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, dto.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, tokenAuthTime.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };
        }
        #endregion
    }
}
