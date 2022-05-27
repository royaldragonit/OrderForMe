using ADO.Extension.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderForMeProject.Commons;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.OrdersModels;
using OrderForMeProject.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Services
{
    public class ReportServices : BaseServices, IReportServices
    {
        public ReportServices(OrderformeContext db, IConfiguration config, IWebHostEnvironment environment, IParameterMapper parameterMapper, IHttpContextAccessor httpContext, IMapper mapper, IUnitOfWork unitOfWork) : base(db, config, environment, parameterMapper, httpContext, mapper, unitOfWork)
        {
        }

        public async Task<ResultCustomModel<InitReport>> GetReport()
        {
            var param = new
            {

            };

            var spParams = _parameterMapper.Map(param);
            var execute = await _db.QueryMultipleResults(sql: "Dr_Report").ExecuteMultipleResults(spParams.Parameters, typeof(int), typeof(int), typeof(int), typeof(int));
            int totalMoney = execute[0][0];
            int totalMoneyWaitForPayment = execute[1][0];
            int totalMoneyDone = execute[2][0];
            int totalMoneyToday = execute[3][0];
            var data = new ResultCustomModel<InitReport>
            {
                Code = 200,
                Success = true,
                Data = new InitReport
                {
                    TotalMoney=totalMoney,
                    TotalMoneyDone=totalMoneyDone,
                    TotalMoneyToday=totalMoneyToday,
                    TotalMoneyWaitForPayment=totalMoneyWaitForPayment
                }
            };
            return data;
        }
    }
    public interface IReportServices
    {
        Task<ResultCustomModel<InitReport>> GetReport();
    }
}
