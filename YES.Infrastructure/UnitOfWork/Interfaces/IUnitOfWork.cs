using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YES.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }

}
