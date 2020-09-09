using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HW3.DAL.Repositories
{
    public interface IRepository<T> where T:TEntity
    {
        Task<List<T>> Get(Expression<Func<T,bool>> filter=null);
        Task<T> Get(int id);
        Task Create(T entity, string createBody = null);
        Task Update(T entity, string modifieBody = null);
        Task Delete(int id);
    }
}
