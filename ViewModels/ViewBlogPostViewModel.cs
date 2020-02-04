using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class ViewBlogPostViewModel
    {
        public User User { get; set; }
        public IList<Blog> Blogs { get; set; }
        public ViewBlogPostViewModel()
        {

        }
    }
}
