using HW3.DAL.Abstracts;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW3.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public Context context;
        public UnitOfWork(Context context)
        {
            this.context = context;
        }
        public IRepository<T> GetRepository<T>() where T : TEntity
        {
            return new Repository<T>(context);
        }
       
        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
