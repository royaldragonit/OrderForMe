using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Commons;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.Login;
using OrderForMeProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Controllers
{
    public class LoginController: Controller
    {
        private ILoginServices _services;
        private readonly OrderformeContext _context;
        public LoginController(ILoginServices services, OrderformeContext context)
        {
            _services = services;
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return Redirect("/Orders");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UsersId,Username,Password,Fullname,Address,Phone,Email,Balance,CreateOn,IsActive")] Users users)
        {
            var roles = await _context.Role.ToListAsync();
            ViewBag.Role = roles;
            users.Balance = 0;
            users.Password = users.Password.ToMD5();
            users.IsActive = true;
            users.CreateOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
        public async Task<IActionResult> ForgetPassword(LoginModel log)
        {
            if (string.IsNullOrEmpty(log.Username) || string.IsNullOrEmpty(log.Email))
                return View();
            LoginModel data = await _services.ForgetPassword(log);
            if (data == null)
            {
                ModelState.AddModelError("NotFound", "Chúng tôi không tìm thấy tài khoản của bạn");
                return View();
            }
            else
            {
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel InfoLogin)
        {

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                _services.Login(InfoLogin);
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult Logout()
        {

            if (User.Identity.IsAuthenticated)
            {
                _services.Logout();
                return RedirectToAction("Index");
            }
            else
            {
                return View("~/Home/Index.cshtml");
            }

        }
    }
}
