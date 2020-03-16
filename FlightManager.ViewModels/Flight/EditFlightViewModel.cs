using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.ViewModels
{
    public class EditFlightViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Departure Location")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a departure location")]
        public string DepartureLocation { get; set; }

        [Display(Name = "Landing Location")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a landing location")]
        public string LandingLocation { get; set; }

        [Display(Name = "Departure Date Time")]
        [DataType(DataType.DateTime)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a departure datetime")]
        public DateTime DepartureDateTime { get; set; }

        [Display(Name = "Landing Date Time")]
        [DataType(DataType.DateTime)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a landing datetime")]
        public DateTime LandingDateTime { get; set; }

        [Display(Name = "Plane Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the type of the plane")]
        public string PlaneType { get; set; }

        [Display(Name = "Plane Unique Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the unique id of the plane")]
        public string PlaneUniqueId { get; set; }

        [Display(Name = "Pilot Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name of the pilot")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "A name should only contain letters")]
        public string PilotName { get; set; }

        [Display(Name = "Regular Seats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the number of regular seats available on the plane")]
        [Range(0, Int32.MaxValue, ErrorMessage = "The number of seats must be more than 0")]
        public int RegularSeats { get; set; }

        [Display(Name = "Business Seats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the number of business seats available on the plane")]
        [Range(0, Int32.MaxValue, ErrorMessage = "The number of seats must be more than 0")]
        public int BusinessSeats { get; set; }
    }
}
