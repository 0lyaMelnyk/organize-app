using AutoMapper;
using HW3.DAL;
using HW3.DAL.Abstracts;
using HW3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Core.Persistence;

namespace HW3.BLL.Tests.Fake
{
    class FakeRepository<T>:Repository<T> where T:TEntity
    {
        public FakeRepository(FakeDbContext context)
            : base(context)
        {
            context.Database.EnsureCreated();
        }
    }
}
