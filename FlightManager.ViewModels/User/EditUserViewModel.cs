using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a username")]
        [RegularExpression("[A-Za-z][A-Za-z0-9._]{4,25}", ErrorMessage = "The username should be between 5 and 25 characters and not start with a number")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a first name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A name should only contain letters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a last name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A name should only contain letters")]
        public string LastName { get; set; }

        [Display(Name = "UCN")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your UCN")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "A UCN should be exactly 10 digits long")]
        public string UCN { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
