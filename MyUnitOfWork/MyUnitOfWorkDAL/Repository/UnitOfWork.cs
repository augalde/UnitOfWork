using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUnitOfWorkDAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly IRepositoryProvider provider;
        private readonly DbContext dbContext;
        private Dictionary<Type, IRepository> repositories;

        public UnitOfWork(DbContext dbContext, IRepositoryProvider provider)
        {
            if (dbContext == null)
                throw new ArgumentNullException();
            if (provider == null)
                throw new ArgumentNullException();
            this.provider = provider;
            this.dbContext = dbContext;
            this.dbContext.Database.CommandTimeout = 120000;
            repositories = new Dictionary<Type, IRepository>();
        }
        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException();
            if (provider == null)
                throw new ArgumentNullException();
            this.dbContext = dbContext;
            this.provider = new RepositoryProvider(this.dbContext);
            
            this.dbContext.Database.CommandTimeout = 120000;
            repositories = new Dictionary<Type, IRepository>();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            Type type = typeof(TEntity);

            try
            {
                if(!repositories.ContainsKey(type))
                {
                    IGenericRepository<TEntity> repository = provider.GetGenericRepository<TEntity>();
                    repositories.Add(type, repository);
                }
                return repositories[type] as IGenericRepository<TEntity>;
            }
            
            catch
            { throw; }
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disponsed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disponsed)
            {
                if(disposing)
                {
                    dbContext.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
