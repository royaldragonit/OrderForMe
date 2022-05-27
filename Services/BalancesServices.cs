using ADO.Extension.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Services
{
    public class BalancesServices : BaseServices, IBalancesServices
    {
        public BalancesServices(OrderformeContext db, IConfiguration config, IWebHostEnvironment environment, IParameterMapper parameterMapper, IHttpContextAccessor httpContext, IMapper mapper, IUnitOfWork unitOfWork) : base(db, config, environment, parameterMapper, httpContext, mapper, unitOfWork)
        {
        }

        public async Task Accept(int LoggerId)
        {
            var logger = _db.Logger.Find(LoggerId);
            var user = _db.Users.Find(logger.UsersId);
            user.Balance += logger.Balance;
            logger.IsAccept = true;
            _db.Update(logger);
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        public void ChargeMoney(int userId, float money)
        {
            var logger = new Logger();
            logger.UsersId = userId;
            logger.Type = "1";
            logger.CreateOn = DateTime.Now;
            logger.Balance = money;
            logger.IsAccept = false;
            _unitOfWork.Balances.Add(logger);
            _unitOfWork.Complete() ;
        }

        public List<Logger> Include()
        {
          var data=  _unitOfWork.Balances.GetQueryAll().Data.Include(x => x.Users).ToList();
            return data;
        }
    }
    public interface IBalancesServices
    {
        Task Accept(int LoggerId);
        void ChargeMoney(int userId, float money);
        List<Logger> Include();
    }
}
