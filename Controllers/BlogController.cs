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
    [Authorize]
   
   
    public class BlogController : Controller
    {
        // GET: /<controller>/
        private readonly AccountDbContext context;
        public BlogController(AccountDbContext dbContext)
        {
            this.context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Blog> blogs = context.Blogs.Include(p => p.Users).ToList();
            return View(blogs);
        }
       
        public IActionResult AddPost()
        {
            NewPostViewModel newPost = new NewPostViewModel();
            return View(newPost);
        }
    
        [HttpPost]
        public IActionResult AddPost(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = context.Users.Single(x => x.ID == model.UserID);
                Blog newPost = new Blog
                {

                    MedicineName = model.MedicineName,
                    Description = model.Description,
                    StreetNumberAndName = model.StreetNumberAndName,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    PostTime = DateTime.Now,
                    Users=newUser
                };
                context.Blogs.Add(newPost);
                context.SaveChanges();
                return Redirect("/Blog");
            }
            return View(model);

        }
        public IActionResult Users(int id)
        {
            if (id == 0)
            {
                return Redirect("Index");
            }
            User thePost = context.Users.Include(x => x.Blogs).Single(x => x.ID == id);
            ViewBag.title = "Users in Blog:" + thePost.Name;
            return View("Index", thePost.Blogs);
        }
        public IActionResult Place()
        {
            return View();
        }
    }
}
