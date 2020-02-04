using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class NewPostViewModel
    {
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Street Number and Name")]
        public string StreetNumberAndName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Insert valid City Name")]
        public string City { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please Insert Valid State Name")]
        public string State { get; set; }
        [Required]
        [RegularExpression("^[0-9]{5,5}$", ErrorMessage = "Please Insert Valid PostCode")]
        public string ZipCode { get; set; }
        public DateTime PostTime { get; set; }

        [Required]
        public int UserID { get; set; }
        public User Name { get; set; }
    }
}
