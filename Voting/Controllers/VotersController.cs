using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Areas.Identity.Models;
using Voting.Areas.Identity.Models.ViewModels;
using Voting.Infrastructure.Interfaces;

namespace Voting.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    public class VotersController : Controller
    {
        private readonly UserManager<Student> userManager;
        private readonly IPictureUpload pictureUpload;
        public VotersController(UserManager<Student> userManager, IPictureUpload pictureUpload)
        {
            this.pictureUpload = pictureUpload;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult AddVoter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVoter(VoterViewModel voter)
        {
            if (!ModelState.IsValid) return View();
            if (await userManager.FindByEmailAsync(voter.Email) != null ||
               await userManager.FindByNameAsync(voter.Username) != null)
            {
                ModelState.AddModelError("", "Email/Username already exists");
                return View(voter);
            }
            var photo = await pictureUpload.UploadAsync(voter.Image);
            Student student = new Student()
            {
                Email = voter.Email,
                StudentId = voter.StuId,
                UserName = voter.Username,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                OtherNames = voter.LastName,
                Image = photo
               
            };
             var result= await userManager.CreateAsync(student, voter.Username);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(student, "Student");
                return RedirectToAction(nameof(ListVoters));
            }

            ModelState.AddModelError("", "Something went wrong");
            return View(voter);
        }
        [HttpGet]
        public IActionResult ListVoters()
        {
            var users = userManager.Users.Where(op => op.StudentId != "1").ToList();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> VoterDetails(string username)
        {
            if (String.IsNullOrEmpty(username)) return View();
            var user =await userManager.FindByNameAsync(username);
            if (user == null) return RedirectToAction(nameof(ListVoters));
            return View(user);
        }
        public async Task<IActionResult> DeleteVoter(string username)
        {
            if (String.IsNullOrEmpty(username)) return View();
            var user = await userManager.FindByNameAsync(username);
            var result=await userManager.DeleteAsync(user);
            if (result.Succeeded) return RedirectToAction(nameof(ListVoters));

            ModelState.AddModelError("", "Something went wrong");
            return RedirectToAction(nameof(ListVoters));
        }
        [HttpGet]
        public async Task<IActionResult> EditVoters(string username)
        {
            if (String.IsNullOrEmpty(username)) return View();
            var user = await userManager.FindByNameAsync(username);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditVoters(VoterViewModel voter)
        {
            if (!ModelState.IsValid) return View();
            if (String.IsNullOrEmpty(voter.Username) ||
                string.IsNullOrEmpty(voter.Email) )
            {
                ModelState.AddModelError("", "Email/Username cannot empty");
                return View(voter);
            }
            var user =await userManager.FindByNameAsync(voter.Username);
            user.Email = voter.Email;
            user.UserName = voter.Username;
            user.OtherNames = voter.OtherNames;
            user.FirstName = voter.FirstName;
            user.LastName = voter.LastName;
            if (voter.Image != null)
            {
                user.Image =await pictureUpload.UploadAsync(voter.Image);
            }
          
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction(nameof(ListVoters));

            ModelState.AddModelError("", "Something went wrong");
            return View(voter);
        }
    }
}
