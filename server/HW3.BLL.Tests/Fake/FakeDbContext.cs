using HW3.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Sqlite;
using HW3.DAL.Abstracts;
using HW3.DAL.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HW3.BLL.Tests
{
    public class FakeDbContext: Context,IDisposable
    {

        public FakeDbContext() : base(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options) {  }
        public FakeDbContext GetDatabaseContext()
        {
            var databaseContext = new FakeDbContext();
            databaseContext.Database.EnsureCreated();           
            return databaseContext;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
