// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OrderForMeProject.Models.Entities
{
    public partial class StateOrder
    {
        public StateOrder()
        {
            Orders = new HashSet<Orders>();
        }

        public int StateOrderId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}