using LiveMotion.Core.DbConfiguration;
using LiveMotion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiveMotion.Core.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private LiveMotionContext _db;

        public BaseRepository(LiveMotionContext db)
        {
            _db = db;
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = _db.Set<T>();
            return query.FirstOrDefault(predicate);
        }

        public virtual void Add<T>(T entity) where T : class
        {
            if (ReferenceEquals(entity, null))
                return;

            _db.Set<T>().Add(entity);
        }

        public virtual void SaveChanges()
        {
            _db.SaveChanges();
        }

        public virtual void Delete<T>(int id) where T : class
        {
            var set = _db.Set<T>();
            var entity = set.Find(id);
            set.Remove(entity);
        }

        public T Find<T>(int id) where T : class
        {
            var set = _db.Set<T>();
            var entity = set.Find(id);
            return entity;
        }

        public List<T> GetAll<T>() where T : class
        {
            var set = _db.Set<T>();
            return set.ToList();
        }

        public List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = _db.Set<T>();
            return query.Where(predicate).ToList();
        }
    }
}
