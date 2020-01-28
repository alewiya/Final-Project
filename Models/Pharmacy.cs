using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Pharmacy
    {
        public string PharmacyName { get; set; }
        public int ID { get; set; }
        public string  Email { get; set; }
        public string Password { get; set; }
        public string DEANumber { get; set; }
        public string NPINumber { get; set; }
        public string StreetNumberAndName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Pharmacy() { }
    }
}
