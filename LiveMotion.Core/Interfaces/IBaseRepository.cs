using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LiveMotion.Core.Interfaces
{
    public interface IBaseRepository
    {
        T Find<T>(int id) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
        List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Add<T>(T entity) where T : class;
        void Delete<T>(int id) where T : class;
        List<T> GetAll<T>() where T : class;
        void SaveChanges();
    }
}
