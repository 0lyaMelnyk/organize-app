using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using HW3.DAL.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using HW3.DAL.Abstracts;
using System.Threading.Tasks;

namespace HW3.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T:TEntity
    {
        private readonly Context context;
        public Repository(Context context)
        {
            this.context = context;
        }
        public async Task<List<T>> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> list = context.Set<T>();
            if (filter != null)
                return await list?.Where(filter).ToListAsync();
            else return await list?.ToListAsync();
        }
        public async Task Create(T entity, string createBody=null)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<T> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity, string updateBody=null)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            T entity = await context.Set<T>().FindAsync(id).ConfigureAwait(false);
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
