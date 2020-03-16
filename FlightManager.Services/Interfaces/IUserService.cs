using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;
using FlightManager.ViewModels;

namespace FlightManager.Services.Interfaces
{
    /// <summary>
    /// The interface that is implemented by the User service
    /// </summary>
    public interface IUserService
    {
        int CreateUser(string username,
                     string password,
                     string email,
                     string firstName,
                     string lastName,
                     string ucn,
                     string address,
                     string phoneNumber
                    );

        ListUsersViewModel ListUsers();

        int EditUser(string id,
                    string username,
                    string email,
                    string firstName,
                    string lastName,
                    string egn,
                    string address,
                    string phoneNumber
                    );

        void DeleteUser(string id);

    }
}
