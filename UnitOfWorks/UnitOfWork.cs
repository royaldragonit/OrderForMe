using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderformeContext _db;
        public UnitOfWork(OrderformeContext context)
        {
            _db = context;
            Balances = new BalancesRepository(_db);
        }

        public IBalancesRepository Balances { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
