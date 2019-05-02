using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voting.Areas.Identity.Models;
using Voting.Models;
using Voting.Models.DbContexts;
using Voting.Models.ViewModels;

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

            var cats = new NumberOfCategoriesViewModel()
            {
                NoOfCategories = Election.Categories.ToList().Count
            };

            return View(cats);
        }
        [HttpGet]
        public IActionResult Vote(int catId)
        {
            var cans=Election.Candidates.Where(p => p.Category.CatId == catId);
            ViewBag.CatId = catId;
            ViewBag.Category = Election.Categories.Single(op => op.CatId == catId).CategoryName;
            return View(cans);
        }
        [HttpPost]
        public async Task<IActionResult> VoteFor(IEnumerable<SelectedVotesPostModel> selectedVotes)
        {

            List<Votes> votes = new List<Votes>();
            foreach(var select in selectedVotes)
            {
                var can =await Election.Candidates.FindAsync(select.CanId);
                var cat =await Election.Categories.FindAsync(select.CatId);
                int voteCount = Election.Votes.Where(p => p.Candidate == can).Where(o => o.Category == cat).Single().VoteCount;
                votes.Add(new Votes()
                {
                    Candidate = can,
                    Category = cat,
                    VoteCount = voteCount + 1
                });
            }
            Election.Votes.UpdateRange(votes);
            string user = httpContext.HttpContext.User.Identity.Name.ToString();
            var student = await userManager.FindByNameAsync(user);

            Election.AlreadyVoted.Add(new AlreadyVoted()
            {
                Student = student,
                Voted = true
            });

            try
            {
            await Election.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Exiting",selectedVotes);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Error()
        {
           // await HttpContext.SignOutAsync("Identity.Application");
            ViewBag.Message = "You Have Already Voted";
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Exiting(IEnumerable<SelectedVotesPostModel> votes)
        {
            List<Votes> ser = new List<Votes>();
            foreach(var v in votes)
            {
                ser.Add(new Votes()
                {
                    Candidate = Election.Candidates.Where(o => o.CanId == v.CanId).Single(),
                    Category = Election.Categories.Where(p => p.CatId == v.CatId).Single()
                });
            }
            await HttpContext.SignOutAsync("Identity.Application");
            return View(ser);
        }
    }
}