using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.ViewModels
{
    public class ListFlightsViewModel
    {
        public IEnumerable<CreateFlightViewModel> Flights { get; set; }
    }
}
