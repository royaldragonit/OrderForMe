using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBalancesRepository Balances { get; }
        int Complete();
    }
}
