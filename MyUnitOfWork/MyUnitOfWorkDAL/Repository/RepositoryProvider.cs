using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUnitOfWorkDAL.Repository
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly DbContext dbContext;

        public RepositoryProvider(DbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new ArgumentNullException("dbcontext", "dbcontext issue");

            }

            this.dbContext = dbContext;
        }
        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(dbContext);
        }
    }
}
