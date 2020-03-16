using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.ViewModels
{
    public class ListUsersViewModel
    {
        public IEnumerable<CreateUserViewModel> Users { get; set; }
    }
}
