using System;
using System.Collections.Generic;
using System.Text;
using HW3.DAL.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using HW3.DAL.Abstracts;

namespace HW3.DAL.Repositories
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<TaskModel> TaskList { get; set; }
        public DbSet<Project> ProjectList { get; set; }
        public DbSet<User> UserList { get; set; }
        public DbSet<Team> TeamList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         var teams = new List<Team>()
            {
               new Team{Id=2,Name="asap"},
                new Team{Id=3,Name="psy"}
            };
            var users = new List<User>()
            {
                new User{Id=1,FirstName="Dekster",LastName="Hanting"},
                new User{Id=2, FirstName="Charly",LastName="Bob",TeamId=1}
            };
            var tasks = new List<TaskModel>()
            {
                new TaskModel{Id=1,Name="Task3",Description="Add seeding data", ProjectId=1,PerformerId=2},
                new TaskModel{Id=2,Name="Task4",Description="Add validation on models",ProjectId=1,PerformerId=1}
            };
            var projects = new List<Project>()
            {
                new Project{Id=1, Name="Arrrrrr",Description="Arrrr",TeamId=1,AuthorId=2}
            };
            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<TaskModel>().HasData(tasks);
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
