using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HW3.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using HW3.DAL.Migrations;
using HW3.DAL.Models;
using HW3.Common;
using System.Threading;

namespace HW3.WebAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.FirstOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<Context>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("Arrr");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<Context>();

                    context.Database.EnsureCreated();

                    var projects = context.ProjectList.ToList();
                    context.ProjectList.RemoveRange(projects);

                    var users = context.UserList.ToList();
                    context.UserList.RemoveRange(users);

                    var tasks = context.TaskList.ToList();
                    context.TaskList.RemoveRange(tasks);

                    var teams = context.TeamList.ToList();
                    context.TeamList.RemoveRange(teams);
                    context.SaveChangesAsync();
                   
                    DataSeeding(context);
                }
            });
            
        }
        public void DataSeeding(Context context)
        {
            var team = new Team()
            {
                Id = 15, 
                Name = "asap"
            };            
            var user = new User()
            {
                Id=15, 
                FirstName="Charly",
                LastName="Bob"
            };
            var tasks = new TaskModel()
            {
                Id=15,
                Name="Task3",
                Description="Add seeding data",
                ProjectId=15
            };
            var project = new Project() 
            {
                Id=15,
                Name="Arrrrrr",
                Description="Arrrr",
            };
            context.AddRange(team,project,tasks,user);
            context.SaveChangesAsync();
        }
    }
}
