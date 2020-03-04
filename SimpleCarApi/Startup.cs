using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCarApi.Services;
using SimpleCarApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Options;


namespace SimpleCarApi
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
          
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureRepositories(services);
            services.AddControllers();
        }

        public virtual void ConfigureRepositories(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<CarsDatabaseSettings>(
                Configuration.GetSection(nameof(CarsDatabaseSettings)));

            services.AddSingleton<ICarsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CarsDatabaseSettings>>().Value);

            services.AddSingleton<ICarService, CarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
