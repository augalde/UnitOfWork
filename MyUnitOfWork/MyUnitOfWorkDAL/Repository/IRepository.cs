using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUnitOfWorkDAL.Repository
{
    public interface IRepository
    {
        Type GetType();
    }
}
