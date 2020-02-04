using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Authorize(Roles ="Pharmacy")]
        public IActionResult Index()
        {
            IList<Blog> blogs = context.Blogs.Include(p => p.User).ToList();
            return View(blogs);
        }
        [Authorize(Roles ="User")]
        public IActionResult AddPost(string name)
        {
            NewPostViewModel newPost = new NewPostViewModel();
            User newUser = context.Users.Single(x => x.Name == User.Identity.Name);
            newPost.UserID = newUser.ID;
            return View(newPost);
        }
    
        [HttpPost]
       [Authorize(Roles = "User")]
        public IActionResult AddPost(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.Where(u => u.ID == model.UserID).FirstOrDefault();
                Blog newPost = new Blog
                {

                    MedicineName = model.MedicineName,
                    Description = model.Description,
                    StreetNumberAndName = model.StreetNumberAndName,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    PostTime = DateTime.Now,
                    UserID=model.UserID,
                    User= user
                };
                context.Blogs.Add(newPost);
                context.SaveChanges();
                return Redirect($"/Users/ViewBlog?id={user.ID}");
                //TempData["id"] = user.ID;
                //return RedirectToAction("/Users/ViewBlog");

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
       
        public IActionResult Edit(int id)
        {
            Blog posts = context.Blogs.Single(c => c.ID == id);
            EditPostViewModel editPost = new EditPostViewModel(posts);
            return View(editPost);
        }
        [HttpPost]
        public IActionResult Edit(EditPostViewModel editPost)
        {
            if (ModelState.IsValid)
            {
                Blog posts = context.Blogs.Single(p => p.ID == editPost.BlogId);
                posts.MedicineName = editPost.MedicineName;
                posts.Description = editPost.Description;
                posts.StreetNumberAndName = editPost.StreetNumberAndName;
                posts.City = editPost.City;
                posts.State = editPost.State;
                posts.ZipCode = editPost.ZipCode;
                posts.PostTime = DateTime.Now;

                context.SaveChanges();
                //return Redirect("Index");

                // find a way to retrieve the user id to pass into the view
                User user = context.Users.Single(x => x.Name == User.Identity.Name);

                return Redirect($"/Users/ViewBlog?id={user.ID}");
            }
            return View(editPost);
        }
        public IActionResult RemovePost()
        {
            ViewBag.title = "Delete Post";
            ViewBag.blogs = context.Blogs.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult RemovePost(int[] blogIds)
        {
            foreach(int blogId in blogIds)
            {
                Blog thePost = context.Blogs.Single(p => p.ID == blogId);
                context.Blogs.Remove(thePost);
            }
            context.SaveChanges();
            User user = context.Users.Single(x => x.Name == User.Identity.Name);

            return Redirect($"/Users/ViewBlog?id={user.ID}");
            //return Redirect("/Index");
        }
    }
}
