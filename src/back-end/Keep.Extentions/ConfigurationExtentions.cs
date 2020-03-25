using Microsoft.Extensions.Configuration;
using System;

namespace Keep
{
    public static class ConfigurationExtentions
    {
        public static IConfiguration BuildForApi(this IConfiguration configuration, string contentRootPath)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(contentRootPath)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            return builder.Build();
        }

        public static IConfiguration BuildForGateway(this IConfiguration configuration, string contentRootPath)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(contentRootPath)
                   .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
