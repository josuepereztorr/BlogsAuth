using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogsAuth.Models;


namespace BlogsAuth.Controllers
{
    public class HomeController : Controller
    {
        private Model.BloggingContext _bloggingContext;
        public HomeController(Model.BloggingContext db) => _bloggingContext = db;

        public IActionResult Index() => View(_bloggingContext.Blogs.OrderBy(b => b.Name));
        
        public IActionResult BlogDetail(int id) => View(new Model.PostViewModel
        {
            blog = _bloggingContext.Blogs.FirstOrDefault(b => b.BlogId == id),
            Posts = _bloggingContext.Posts.Where(p => p.BlogId == id)
        });
        
        public IActionResult AddBlog() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBlog(Model.Blog model)
        {
            if (ModelState.IsValid)
            {
                if (_bloggingContext.Blogs.Any(b => b.Name == model.Name))
                {
                    ModelState.AddModelError("", "Name must be unique");
                }
                else
                {
                    _bloggingContext.AddBlog(model);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        
        public IActionResult DeleteBlog(int id)
        {
            _bloggingContext.DeleteBlog(_bloggingContext.Blogs.FirstOrDefault(b => b.BlogId == id));
            return RedirectToAction("Index");
        }
        
        public IActionResult AddPost(int id)
        {
            ViewBag.BlogId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(int id, Model.Post post)
        {
            post.BlogId = id;
            if (ModelState.IsValid)
            {
                _bloggingContext.AddPost(post);
                return RedirectToAction("BlogDetail", new { id = id });
            }
            @ViewBag.BlogId = id;
            return View();
        }
        
        public IActionResult DeletePost(int id)
        {
            Model.Post post = _bloggingContext.Posts.FirstOrDefault(p => p.PostId == id);
            int BlogId = post.BlogId;
            _bloggingContext.DeletePost(post);
            return RedirectToAction("BlogDetail", new { id = BlogId });
        }
    }
}

