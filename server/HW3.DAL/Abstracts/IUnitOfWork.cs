using HW3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.DAL.Abstracts
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : TEntity;
    }
}
