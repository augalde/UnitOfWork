
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUnitOfWorkDAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;
        private DbSet<TEntity> dbSet = null;

        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbcontext","dbContext is mandatory to use GenericRepository");
            }

            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity", "entity is required to perform an add");

            }

            try
            {
                dbSet.Add(entity);
            }
            catch
            { throw; }
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is required to perform an add");

            }

            try
            {
                dbSet.Attach(entity);
            }
            catch
            { throw; }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is required to perform an add");

            }

            try
            {
                if(dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            catch
            { throw; }
        }

        public IQueryable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate", "predicate null");
            }

            try { 
            IQueryable<TEntity> result = dbSet.Where(predicate);
                return result;
                }
            catch { throw; }
         }

        public TEntity FindSingle(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate", "predicate null");
            }

            try
            {
                return FindAll(predicate).SingleOrDefault();
            }
            catch { throw; }
            }

        public IQueryable<TEntity> GetAll()
        {
            return this.dbSet;
        }

        public TEntity GetByID(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is required to perform an add");

            }

            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
