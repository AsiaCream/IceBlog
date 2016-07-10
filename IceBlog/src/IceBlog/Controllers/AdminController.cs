using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IceBlog.Models;

namespace IceBlog.Controllers
{
    public class AdminController : Controller
    {
        private AdminController(UserManager<User> userManager,SignInManager<User> signInManager,BlogContext db)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            DB = db;
        }

        private BlogContext DB { get; set; }

        private UserManager<User> UserManager { get; }

        private SignInManager<User> SignInManager { get; }

        /// <summary>
        /// 渲染登陆页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 执行登陆请求
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string username,string password)
        {
            var result = await SignInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
        /// <summary>
        /// 登出操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 渲染添加文章类型页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult CreateCategory()
        {
            return View(DB.Categorys
                .OrderBy(x => x.Id)
                .ToList());
        }
        /// <summary>
        /// 提交POST方法创建文章类型
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory(Category category)
        {
            DB.Categorys.Add(category);
            DB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 渲染修改文章类型页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult EditCategory(long id)
        {
            var category = DB.Categorys
                .Where(x => x.Id == id)
                .SingleOrDefault();
            //先判断要修改的文章类型是否存在
            //如果不存在跳转至错误页面
            //存在则返回相应信息
            if (category == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(category);
            }
        }
        /// <summary>
        /// 执行POST请求修改文章类型页面
        /// </summary>
        /// <param name="id">要修改的类型ID</param>
        /// <param name="category">修改的内容</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult EditCategory(long id, Category category)
        {
            var oldcategory = DB.Categorys
                .Where(x => x.Id == id)
                .SingleOrDefault();
            //执行修改的过程中
            //先判断文章类型是否还存在
            //如果不存在就给出错误信息
            //存在则执行修改
            if (oldcategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                oldcategory.Priority = category.Priority;
                oldcategory.Title = category.Title;
                DB.SaveChanges();
                return Content("success");
            }
        }
        /// <summary>
        /// 删除文章类型方法
        /// </summary>
        /// <param name="id">要修改的文章ID</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult DeleteCategory(long id)
        {
            var category = DB.Categorys
                .Where(x => x.Id == id)
                .SingleOrDefault();
            //先判断要删除的文章类型是否还存在
            //如果存在则可以删除
            //不存在则反馈错误信息
            if (category == null)
            {
                return Content("未找到对应文章类型");
            }
            else
            {
                DB.Categorys.Remove(category);
                DB.SaveChanges();
                return Content("success");
            }
        }
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [Authorize]
        private Task<User> GetUserCurrent()
        {
            return UserManager.GetUserAsync(HttpContext.User);
        }
    }
}
