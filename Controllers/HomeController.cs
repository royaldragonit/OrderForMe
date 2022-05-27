using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderForMeProject.Models;
using OrderForMeProject.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Controllers
{
    public class HomeController: BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportServices _reportServices;

        public HomeController(ILogger<HomeController> logger, IReportServices reportServices)
        {
            _logger = logger;
            _reportServices = reportServices;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _reportServices.GetReport();
            return View(model.Data);
        }
        public IActionResult ManageUsers()
        {
            return View();
        }
        public IActionResult ManageGroupProduct()
        {
            return View();
        }
        public IActionResult ManageRevenue()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
