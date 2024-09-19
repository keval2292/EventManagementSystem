using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EMS.DB.unitofwork
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(long id);
      
        //params Expression<Func<T, object>>[] includes
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}