using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Models.OrdersModels
{
    public class SplitOrdersModel
    {
        public string Link { get; set; }
        public float Price { get; set; }
        public int LinkProductId { get; set; }
        public int Quantity { get; set; }
    }
}
