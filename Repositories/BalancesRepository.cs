using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Repositories
{
    public class BalancesRepository : GenericRepository<Logger>, IBalancesRepository
    {
        public BalancesRepository(OrderformeContext db) : base(db)
        {
        }
    }
}
