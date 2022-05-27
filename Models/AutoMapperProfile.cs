using AutoMapper;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrdersModel, Orders>().ForMember(x => x.LinkProduct, opt => opt.Ignore());
        }
    }
}
