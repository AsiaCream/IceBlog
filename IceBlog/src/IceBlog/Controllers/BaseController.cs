using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using IceBlog.Models;

namespace IceBlog.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, SignInManager<User> signInManager,BlogContext db)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            DB = db;
        }

        public BlogContext DB { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public User UserCurrent { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                UserCurrent = DB.Users
                .Where(x => x.UserName == HttpContext.User.Identity.Name)
                .SingleOrDefault();
                ViewBag.UserCurrent = UserCurrent;
            }
        }
    }
}
