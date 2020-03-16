using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Interfaces;
using FlightManager.Services;
using FlightManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.Controllers
{
    public class UserController : Controller
    {
        private FlightManagerDbContext context;
        private IUserService service;
        private readonly UserManager<AppUser> userManager;

        public UserController(FlightManagerDbContext context, IUserService service, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.service = service;
            this.userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateUser()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult CreateUser(string username, string password, string email, string firstName, string lastName, string ucn, string address,
            string phoneNumber)
        {
            if (ModelState.IsValid)
            {
                var createUser = service.CreateUser(username, password, email, firstName, lastName, ucn, address, phoneNumber);
                if (createUser == -1)
                {
                    ViewBag.Message = "Username already taken!";
                    return View();
                }
            }
            return RedirectToAction("ListUsers", "User");
        }

        [Authorize(Roles = "admin")]
        public IActionResult ListUsers()
        {
            ViewData["ListUsers"] = service.ListUsers();
            ViewData["UserManager"] = userManager;
            ViewData["Context"] = context;
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditUser(string id)
        {
            var user = context.Users.Find(id);

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UCN = user.UCN,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (service.EditUser(model.Id, model.Username, model.Email, model.FirstName, model.LastName, model.UCN, model.Address, model.PhoneNumber) == -1)
                {
                    ViewBag.Message = "Username already taken!";
                    return View();
                }
            }
            return this.RedirectToAction("ListUsers", "User");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteUser(string id)
        {
            service.DeleteUser(id);
            return this.RedirectToAction("ListUsers", "User");
        }
    }
}