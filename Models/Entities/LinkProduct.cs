// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OrderForMeProject.Models.Entities
{
    public partial class LinkProduct
    {
        public int LinkProductId { get; set; }
        public string Link { get; set; }
        public int OrdersId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int TotalMoney { get; set; }
        public int RateUsdtoVnd { get; set; }

        public virtual Orders Orders { get; set; }
    }
}