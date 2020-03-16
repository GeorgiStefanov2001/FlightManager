using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.ViewModels;
using FlightManager.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.Services
{
    public class UserService : IUserService
    {
        private FlightManagerDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserService(FlightManagerDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public int CreateUser(string username,
                            string password,
                            string email,
                            string firstName,
                            string lastName,
                            string ucn,
                            string address,
                            string phoneNumber
                            )
        {

            var user = new AppUser()
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UCN = ucn,
                Address = address,
                PhoneNumber = phoneNumber,
            };

            if (userManager.FindByNameAsync(username).Result == null)
            {
                var createdUser = userManager.CreateAsync(user, password); 
                if (createdUser.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "employee").Wait();
                    context.SaveChanges();
                    return 1;
                }
            }
            return -1;

        }
        public ListUsersViewModel ListUsers()
        {
            var users = context.Users.Select(j => new CreateUserViewModel()
            {
                Id = j.Id,
                Username = j.UserName,
                Email = j.Email,
                FirstName = j.FirstName,
                LastName = j.LastName,
                UCN = j.UCN,
                Address = j.Address,
                PhoneNumber = j.PhoneNumber
            });


            var model = new ListUsersViewModel() { Users = users };

            return model;
        }
        public int EditUser(string id,
                    string username,
                    string email,
                    string firstName,
                    string lastName,
                    string ucn,
                    string address,
                    string phoneNumber
            )
        {

            var foundUser = userManager.FindByNameAsync(username).Result;
            if (foundUser!= null)
            {
                if(foundUser.Id != id)
                {
                    return -1;
                }

            }

            var updateUser = userManager.FindByIdAsync(id).Result;
            updateUser.UserName = username;
            updateUser.Email = email;
            updateUser.FirstName = firstName;
            updateUser.LastName = lastName;
            updateUser.UCN = ucn;
            updateUser.Address = address;
            updateUser.PhoneNumber = phoneNumber;

            userManager.UpdateAsync(updateUser).Wait();
            context.SaveChanges();

            return 1;
        }

        public void DeleteUser(string id)
        {
            var user = context.Users.Find(id);

            if (user != null)
            {
                userManager.DeleteAsync(user).Wait();
                context.SaveChanges();
            }
        }
    }
}

