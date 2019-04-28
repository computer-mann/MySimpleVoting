using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voting.Areas.Identity.Models;
using Voting.Models.DbContexts;

namespace Voting.Controllers
{
    [Authorize(Roles ="Student")]
    [Route("[controller]/[action]")]
    public class VotingController : Controller
    {
        public ElectionDbContext  Election { get; set; }
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<Student> userManager;
        public VotingController(ElectionDbContext electionDbContext, IHttpContextAccessor httpContexts, UserManager<Student> userManagers)
        {
            Election = electionDbContext;
            httpContext = httpContexts;
            userManager = userManagers;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cats = Election.Categories.ToList();
            return View(cats);
        }
        [HttpGet]
        public IActionResult Vote(int catId)
        {
            var cans=Election.Candidates.Where(p => p.Category.CatId == catId);
            ViewBag.CatId = catId;
            return View(cans);
        }
        //[HttpPost]
        public async Task<IActionResult> VoteFor(int canId, int catId)
        {
            var can =await Election.Votes.Where(c => c.Candidate.CanId == canId).Where(d => d.Category.CatId == catId).FirstAsync();
            can.VoteCount = can.VoteCount + 1;
            string user = httpContext.HttpContext.User.Identity.Name;
            var student = await userManager.FindByNameAsync(user);

            Election.AlreadyVoted.Add(new Models.AlreadyVoted()
            {
                Student = student,
                Voted = true
            });

            Election.Update(can);
            await Election.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}