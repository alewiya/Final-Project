using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Blog
    {
        public string  MedicineName { get; set; }
        public string Description { get; set; }
        public string StreetNumberAndName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime PostTime { get; set; }
        public int ID { get; set; }
       //foregin key
        public int UserID { get; set; }
        public User User { get; set; }//navgation property
        public string Name { get; set; }
    }
}
