using ADO.Extension.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderForMeProject.Commons;
using OrderForMeProject.Interfaces;
using OrderForMeProject.Models.CustomModels;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Models.Login;
using OrderForMeProject.Models.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrderForMeProject.Services
{
    public class LoginServices : BaseServices, ILoginServices
    {
        public LoginServices(OrderformeContext db, IConfiguration config, IWebHostEnvironment environment, IParameterMapper parameterMapper, IHttpContextAccessor httpContext, IMapper mapper, IUnitOfWork unitOfWork) : base(db, config, environment, parameterMapper, httpContext, mapper, unitOfWork)
        {
        }

        public async Task<LoginModel> ForgetPassword(LoginModel log)
        {
            log.Email = log.Email.Trim().ToLower();
            log.Username = log.Username.Trim().ToLower();
            Users user = await _db.Users.FirstOrDefaultAsync(x=>x.Username==log.Username&&x.Email==log.Email);
            if (user == null)
                return null;
            log.Password=Guid.NewGuid().ToString();
            user.Password = log.Password.ToMD5();
            await _db.SaveChangesAsync();
            return log;
        }

        public async Task Login(LoginModel infoLogin)
        {
            infoLogin.Password = infoLogin.Password.ToMD5();
            Users user =  _db.Users.Where(x => x.Username == infoLogin.Username && x.Password == infoLogin.Password).Include(x=>x.Role).FirstOrDefault();        
            if (user != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UsersId.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, infoLogin.Username));
                identity.AddClaim(new Claim("FullName", user.Fullname));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role,user.Role.Name.ToString()));
                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                    IsPersistent = true,
                };
                await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);
            }
            else
            {
                return;
            }

        }

        public async Task Logout()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
    public interface ILoginServices
    {
        Task<LoginModel> ForgetPassword(LoginModel log);
        Task Login(LoginModel login);
        Task Logout();
    }
}
