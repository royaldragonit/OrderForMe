using Microsoft.AspNetCore.Mvc.Rendering;
using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Models.OrdersModels
{
    public class InitCreateOrders
    {
        public List<SelectListItem> ListSourceBuy { get; set; }
        public List<SiteBuy> ListSiteBuy { get; set; }
        public List<SelectListItem> ListCustomer { get; set; }
        public List<SelectListCustom> ListGroupProduct { get; set; }
        public List<SelectListItem> ListStateOrders { get; set; }
        public List<LinkProduct> ListLinkProduct { get; set; }

        public Setting Settings { get;  set; }
        public Orders Order { get;  set; }
    }
}
