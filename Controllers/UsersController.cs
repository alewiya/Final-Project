using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
 //[Authorize]
    public class UsersController : Controller
    {
        // GET: /<controller>/

        private readonly AccountDbContext context;
        public UsersController(AccountDbContext dbContext)
        {
           this.context = dbContext;
        }
       //[Authorize]
        public IActionResult Index()
        {
            List<User> users= context.Users.ToList();
            return View(users);
              
        }
        public IActionResult ViewBlog(int id)
        {
            if (id == 0)
            {
                return View();
            }
            User newPost = context.Users.Single(x => x.ID == id);
            IList<Blog> blogs = context.Blogs.Where(x => x.UserID == id).ToList();
            return View(blogs);
        }

        
        
    }
}
