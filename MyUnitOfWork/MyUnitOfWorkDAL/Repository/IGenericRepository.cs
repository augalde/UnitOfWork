using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyUnitOfWorkDAL.Repository
{
    public interface IGenericRepository<TEntity> : IRepository
    {
        TEntity GetByID(object id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void Attach(TEntity entity);
        void Delete(TEntity entity);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity FindSingle(Expression<Func<TEntity, bool>> predicate);

    }
}
