using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.ViewModels
{
    public class ListReservationsViewModel
    {
        public IEnumerable<CreateReservationViewModel> Reservations { get; set; }
    }
}
