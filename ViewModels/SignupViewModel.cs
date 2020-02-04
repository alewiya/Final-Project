using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class SignupViewModel
    {

        [Required]
        [Display (Name ="Name Of Pharmacy")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Please Insert letter gerater than three and less than twenty")]
       // [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please Insert a valid Pharmacy Name")]
        public string PharmacyName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = " Please Insert Valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string Verfiy { get; set; }
      [Required]
      [Display(Name ="Durg Enforcement Agency Number")]
      [RegularExpression("^[A-Za-z]{2}[0-9]{6}")]
        public string DEANumber { get; set; }
       [Required]
       [Display(Name ="National Provider Identifier Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = " Please Insert Valid National Privider Identifier Number")]
        [RegularExpression("^[0-9]{10,10}$")]
        public string  NPINumber { get; set; }
        [Required]
        [Display(Name = "Street Number and Name")]
        //[RegularExpression("^[0-9{1,5}$+ [^ ]+^[a-zA-Z]+$", ErrorMessage = "Please Insert Correct Street Number and Street Name" )]
        public string StreetNumberAndName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage= "Please Insert valid City Name")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Insert State Name")]
        public string State { get; set; }
        [Required]
        [RegularExpression("^[0-9]{5,5}$", ErrorMessage = "Please Insert Valid PostCode")]
        public string ZipCode { get; set; }
        public SignupViewModel() { }

    }
}
