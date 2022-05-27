using ADO.Extension.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Services
{
    public abstract class BaseServices
    {
        public readonly OrderformeContext _db;
        public readonly OrderformeContextProcedures _sp;
        public readonly IConfiguration _config;
        public readonly string _host;
        public readonly IWebHostEnvironment _environment;
        public IParameterMapper _parameterMapper;
        public readonly IHttpContextAccessor _httpContext;
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public BaseServices(OrderformeContext db, IConfiguration config, IWebHostEnvironment environment, IParameterMapper parameterMapper, IHttpContextAccessor httpContext, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _parameterMapper = parameterMapper;
            _db = db;
            _sp = new OrderformeContextProcedures(_db);
            _config = config;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
