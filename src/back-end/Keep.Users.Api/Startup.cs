using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keep.Users.Services.Abstractions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Keep.Users.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            Configuration =  Configuration.BuildForApi(env.ContentRootPath);  
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<AuthOptions>(settings => 
            {
                Configuration.GetSection("Auth").Bind(settings);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
              
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
