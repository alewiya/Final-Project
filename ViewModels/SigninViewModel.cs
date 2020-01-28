 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class SigninViewModel
    {
        [Required]
        [Display(Name = "Name Of Pharmacy")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please Insert letter gerater than three and less than twenty")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please Insert a valid Pharmacy Name")]
        public string PharmacyName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = " Please Insert Valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public SigninViewModel() { }
    }
}
