using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Models.Report
{
    public class InitReport
    {
        public int TotalMoney { get; set; }
        public int TotalMoneyToday { get; set; }
        public int TotalMoneyWaitForPayment { get; set; }
        public int TotalMoneyDone { get; set; }
    }
}
