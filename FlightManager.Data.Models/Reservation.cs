using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int FlightId { get; set; }

        public string FeedBackEmail { get; set; }

        public int RegularSeats { get; set; }

        public int BusinessSeats{ get; set; }
    }
}
