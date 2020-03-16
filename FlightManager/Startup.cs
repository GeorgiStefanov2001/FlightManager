using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FlightManager.Data.Models;
using FlightManager.Data;
using FlightManager.Services;
using FlightManager.Services.Interfaces;

namespace FlightManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlightManagerDbContext>(options =>
                options.UseSqlServer(ConfigurationData.ConnectionString));
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FlightManagerDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IReservationService, ReservationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, FlightManagerDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("admin")).Wait();
            }
            if (!roleManager.RoleExistsAsync("employee").Result)
            {
                roleManager.CreateAsync(new IdentityRole("employee")).Wait();
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!context.Users.Any())
            {
                //Create the one and only admin
                var user = new AppUser()
                {
                    UserName = Configuration.GetSection("AdminCredentials")["Username"],
                    Email = Configuration.GetSection("AdminCredentials")["Email"],
                    FirstName = Configuration.GetSection("AdminCredentials")["FirstName"],
                    LastName = Configuration.GetSection("AdminCredentials")["LastName"],
                    UCN = Configuration.GetSection("AdminCredentials")["UCN"],
                    Address = Configuration.GetSection("AdminCredentials")["Address"],
                    PhoneNumber = Configuration.GetSection("AdminCredentials")["PhoneNumber"],
                };

                var pass = Configuration.GetSection("AdminCredentials")["Password"];
                var createdUser = userManager.CreateAsync(user, pass);

                if (createdUser.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                    context.SaveChanges();
                }
            }
        }
    }
}
