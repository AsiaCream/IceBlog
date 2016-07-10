using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IceBlog.Models;


namespace IceBlog.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, BlogContext db)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            DB = db;
        }

        private BlogContext DB { get; set; }

        private UserManager<User> UserManager { get; }

        private SignInManager<User> SignInManager { get; }
        /// <summary>
        /// 博客首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
