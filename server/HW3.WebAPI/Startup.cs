using HW3.BLL.Services;
using HW3.BLL.ServicesAbstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HW3.BLL;
using AutoMapper;
using HW3.DAL.Repositories;
using System.Collections.Generic;
using HW3.DAL;
using System.Threading.Tasks;
using HW3.DAL.Models;
using HW3.DAL.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
namespace HW3.WebAPI
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
            //services.AddTransient<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("OrganizationOfWorkDatabase")));

           services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("OrganizationOfWorkDatabase")), ServiceLifetime.Singleton);
           // services.AddDbContext<Context>(ServiceLifetime.Transient);

            services.AddAutoMapper(typeof(ConfigurationMapper));
            services.AddCors();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

        }
    }
}
