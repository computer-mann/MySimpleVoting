using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Voting.Areas.Identity.Models;
using Voting.Areas.Identity.Models.ViewModels;

namespace Voting.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("auth/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<Student> userManager;
        private readonly SignInManager<Student> signinManager;

        public AccountController(UserManager<Student> userManager,SignInManager<Student> signIn)
        {
            signinManager = signIn;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]
        [Bind(include:"UserName,StudentId,Password")]LoginViewModel model,string returnUrl)
        {
           
            if (!ModelState.IsValid)
            {
            return View(model);
            }
            Student user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (user.StudentId == model.StudentId)
                {
                    var result =await signinManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                     
                    }
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            if ((signinManager.SignOutAsync().IsCompletedSuccessfully)){
              return  Redirect("/");
            }
            return BadRequest();
        }
    }
}