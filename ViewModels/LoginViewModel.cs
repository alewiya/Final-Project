using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please Insert letter gerater than three and less than fifteen")]
       // [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please Insert a valid User Name")]

        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please Insert Valid Password")]

        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public LoginViewModel() { }
    }
}
