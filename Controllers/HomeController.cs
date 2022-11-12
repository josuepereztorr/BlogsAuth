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
    }
}