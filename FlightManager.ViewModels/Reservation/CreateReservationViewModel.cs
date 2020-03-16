using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.ViewModels
{
    public class CreateReservationViewModel
    {
        public int Id { get; set; }

        public int FlightId { get; set; }

        [Display(Name = "Feedback Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an e-mail")]
        [DataType(DataType.EmailAddress)]
        public string FeedBackEmail { get; set; }

        [Display(Name = "Regular Seats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the number of regular seats available on the plane")]
        [Range(0,Int32.MaxValue, ErrorMessage = "The number of seats must be more than 0")]
        public int RegularSeats { get; set; }

        [Display(Name = "Business Seats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the number of business seats available on the plane")]
        [Range(0, Int32.MaxValue, ErrorMessage = "The number of seats must be more than 0")]
        public int BusinessSeats { get; set; }
    }
}
