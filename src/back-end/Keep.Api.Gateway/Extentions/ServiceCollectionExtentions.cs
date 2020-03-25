using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keep.Api.Gateway 
{
    public static class ServiceCollectionExtentions
    {
        #region Contants
        private const string AuthSchemeName = "JWT";
        #endregion

        private static IConfiguration _configuration;

        public static IServiceCollection Init(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            var audienceConfig = _configuration.GetSection("Auth");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"])),
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = AuthSchemeName;
            })
            .AddJwtBearer(AuthSchemeName, x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        } 
    }
}
