using OrderForMeProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Models.OrdersModels
{
    public class CreateOrdersModel
    {
        public int OrdersId { get; set; }
        public int SourceBuyId { get; set; }
        public int SiteBuyId { get; set; }
        public int UsersId { get; set; }
        public int GroupProductId { get; set; }
        public float[] Price { get; set; }
        public float Size { get; set; }
        public float OtherFee { get; set; }
        public string Note { get; set; }
        public string Currency { get; set; }
        public int RateUsdvnd { get; set; }
        public int FeeShipping { get; set; }
        public int Tax { get; set; }
        public int FeeShippingToVn { get; set; }
        public int FeeServices { get; set; }
        public int FeeServicesOther { get; set; }
        public int TotalMoney { get; set; }
        public bool IsShippingToVn { get; set; }
        public string PaymentType { get; set; }
        public bool IsCompleted { get; set; }
        public int StateOrderId { get; set; }
        public string CreateBy { get; set; }
        public DateTime DateBuy { get; set; }
        public DateTime CreateOn { get; set; }
        public string[] LinkProduct { get; set; }
        public int[] Quantity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
