using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Voting.Areas.Identity.Models;
using Voting.Areas.Identity.Models.ViewModels;

namespace Voting.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("admin/[controller]/[action]")]
    public class AdminsController : Controller
    {
        private readonly UserManager<Student> userManager;
        private readonly SignInManager<Student> signInManager;
        public AdminsController(UserManager<Student> userManager,SignInManager<Student> signIn)
        {
            this.userManager = userManager;
            signInManager = signIn;
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginViewModel admin)
        {
            if (ModelState.IsValid)
            {
                Student user =await userManager.FindByNameAsync(admin.Username);
                if (user != null)
                {
                    if(user.StudentId != admin.AdminId)
                    {
                        ModelState.AddModelError("", "Invalid Admin Id");
                        return View(admin);
                    }
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        ModelState.AddModelError("", "You are not an admin");
                        return View(admin);
                    }
                    var result = await signInManager.PasswordSignInAsync(user, admin.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect("~/admin/categories/list");
                    }
                }
            }
            ModelState.AddModelError("", "Something Went Wrong");
            return View(admin);
        }
    }
}