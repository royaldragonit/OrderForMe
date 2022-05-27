
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OrderForMeProject.Models.Entities;
using OrderForMeProject.Services;
using ADO.Extension.Utils;
using ADO.Extension.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using OrderForMeProject.Models;
using Microsoft.AspNetCore.HttpOverrides;
using OrderForMeProject.Interfaces;
using OrderForMeProject.UnitOfWorks;
using OrderForMeProject.Repositories;

namespace OrderForMeProject
{
    public class Startup
    {
        private readonly string Cors = "Cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllers();
            services.AddRazorPages();
            services.AddAutoMapper
(typeof(AutoMapperProfile).Assembly);
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).AddViewLocalization();
            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.AccessDeniedPath = new PathString("/Login");
                o.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                o.SlidingExpiration = true;
                o.ExpireTimeSpan = DateTime.Now.AddDays(1).TimeOfDay;
                o.LoginPath = new PathString("/Login");
                o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                o.SlidingExpiration = true;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chathub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #region Ngăn chặn các Website khác lấy dữ liệu từ trang web
            services.AddCors(options =>
            {
                options.AddPolicy(name: Cors,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });
            #endregion
            #region Inject Services
            services.AddDbContext<OrderformeContext>(options =>
                                                    options.UseSqlServer(
                                                        Configuration.GetConnectionString("DefaultConnection")
                                                    ));
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IParameterMapper, ParameterMapper>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IReportServices, ReportServices>();
            #endregion
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IBalancesRepository, BalancesRepository>();
            #endregion
            #region UnitOfWork

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
            services.AddTransient<IBalancesServices, BalancesServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            //Cho phép sử dụng CORS
            app.UseCors(Cors);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute
                (
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}/{slug?}"
                );

                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}"
                );
            });
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 401 || context.Response.StatusCode == 403)
                {
                    context.Request.Path = "/Login";
                    await next();
                }
            });
        }
    }
}
