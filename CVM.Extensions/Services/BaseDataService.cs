using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CVM.Extensions.Services
{
    abstract class BaseDataService<T> : IRepository<T> where T : class, IEntity
    {
        internal readonly DebugDatacontext _db;

        public BaseDataService(DebugDatacontext db)
        {
            _db = db;
        }

        public virtual Task<List<T>> GetAll()
        {
            return _db.Set<T>().ToListAsync();
        }


        public virtual async Task<T> Create(T newEntry)
        {
            _db.Set<T>().Add(newEntry);
            await _db.SaveChangesAsync();
            return newEntry;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }



        public virtual async Task<T> Update(T entity)
        {
            var res = await _db.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            res = entity;

            _db.Set<T>().Update(res);
            await _db.SaveChangesAsync();
            return res;
        }

        public virtual Task<T> GetById(int id)
        {
            return _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> Filter()
        {
            return _db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Filter(Func<T, bool> predicate)
        {
            return _db.Set<T>().Where(predicate);
        }



        public virtual async Task<bool> Delete(int id)
        {
            var res = await _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            _db.Set<T>().Remove(res);
            await _db.SaveChangesAsync();

            return true;
        }
    }




    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Delete(int id);
        Task<TEntity> Update(TEntity entity);
        Task<List<TEntity>> GetAll();
        //read side (could be in separate Readonly Generic Repository)
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> Filter();
        Task<IEnumerable<TEntity>> Filter(Func<TEntity, bool> predicate);


        //separate method SaveChanges can be helpful when using this pattern with UnitOfWork

    }

    public interface IEntity
    {
        int Id { get; set; }
    }
}
