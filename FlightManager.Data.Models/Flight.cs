using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Flight
    {
        public int Id { get; set; }

        public string DepartureLocation { get; set; }

        public string LandingLocation { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime LandingDateTime { get; set; }

        public string PlaneType { get; set; }

        public string PlaneUniqueId { get; set; }

        public string PilotName { get; set; }

        public int RegularSeats { get; set; }

        public int BusinessSeats { get; set; }
    }
}
