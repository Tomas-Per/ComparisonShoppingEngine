using DataContent.DAL.Interfaces;
using DataContent.DAL.Repositories;
using ModelLibrary.DataContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraAPI
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
            services.AddDbContext<ComputerContext>(options =>
                                   options.UseSqlServer(Configuration.GetConnectionString("LibraDB")));
            services.AddDbContext<SmartphoneContext>(options =>
                                   options.UseSqlServer(Configuration.GetConnectionString("LibraDB")));
            services.AddDbContext<UserContext>(options =>
                                    options.UseSqlServer(Configuration.GetConnectionString("LibraDB")));
            services.AddControllers();
                     
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.1", new OpenApiInfo { Title = "LibraAPI", Version = "v1.1" });
            });
            services.AddScoped<IComputerRepository, ComputerRepository>();
            services.AddScoped<IProcessorRepository, ProcessorRepository>();
            services.AddScoped<ISmartphoneRepository, SmartphoneRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IFavoriteItemRepository, FavoriteItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "LibraAPI v1.1"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
