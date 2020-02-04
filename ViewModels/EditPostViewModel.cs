using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class EditPostViewModel:NewPostViewModel
    {
        public int BlogId { get; set; }
        public EditPostViewModel()
        {

        }
        public EditPostViewModel(Blog posts)
        {
            BlogId = posts.ID;
            MedicineName = posts.MedicineName;
            Description = posts.Description;
            StreetNumberAndName = posts.StreetNumberAndName;
            City = posts.City;
            State = posts.State;
            ZipCode = posts.ZipCode;

        }
    }
}
