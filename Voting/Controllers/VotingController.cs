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
using Microsoft.Extensions.Caching.Memory;
using Voting.Areas.Identity.Models;
using Voting.Infrastructure.Interfaces;
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
        private readonly int noOfCats;
        private readonly IExitingModel exitingModel;
        private readonly IMemoryCache cache;
        
        public VotingController(IExitingModel exiting,
            ElectionDbContext electionDbContext, IHttpContextAccessor httpContexts,
            UserManager<Student> userManagers,IMemoryCache memory)
        {
            Election = electionDbContext;
            httpContext = httpContexts;
            userManager = userManagers;
            exitingModel = exiting;
            noOfCats = Election.Categories.Count();
            cache = memory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var cats =await Election.Categories.AsNoTracking().ToListAsync();
            var cats = await Election.Categories.Where(p => p.Election.Ongoing == true).ToListAsync();
            string user = httpContext.HttpContext.User.Identity.Name.ToString();
            List<Category> listcat = new List<Category>();
            foreach(var cat in cats)
            {
                if (cache.Get($"{user}-{cat.CatId}") == null)
                {
                    listcat.Add(cat);
                }
            }
            return View(listcat);
        }
        [HttpGet]
        public async Task<IActionResult> CatIndex(int? catId)
        {
            if (catId == null) return BadRequest();
            var cans = Election.Candidates.Where(p => p.Category.CatId == catId.Value).ToList();
            ViewBag.Cat = catId;
            var catego =await Election.Categories.Include(op => op.Election).Where(p => p.CatId == catId).FirstAsync();
            ViewBag.CategoryNameFull = catego.CategoryName;
            ViewBag.Guid = catego.Election.Id;
            ViewBag.CategoryName = catego.CategoryName.Split(' ').Last();
            return View(cans);
        }

        [HttpPost]
        public async Task<IActionResult> VoteFor(SelectedVotesPostModel selectedVotes,Guid guid)
        {
            var vote = Election.Votes.Find(selectedVotes.CatId, selectedVotes.CanId);
            int voteCount = Election.Votes.AsNoTracking().Single(p => p.CandidateId == selectedVotes.CanId).VoteCount;
            vote.VoteCount = voteCount + 1;
            Election.Votes.Update(vote);
            string user = httpContext.HttpContext.User.Identity.Name.ToString();
            var student = await userManager.FindByNameAsync(user);
            cache.Set($"{user}-{selectedVotes.CatId}", "");

            if (Election.AlreadyVoted.Find(student.Id,guid) != null)
            {
                return RedirectToAction("Error");
            }
            exitingModel.AddSelectedVotesPosts(selectedVotes);
            try
            {
                await Election.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", new { message = e.Message });
            }
            //if (selectedVotes.CatId < noOfCats)
            //{
            //    var caty = selectedVotes.CatId + 1;
            //    return RedirectToAction(nameof(Index), new { catId = caty });
            //}
            try
            {
                await Election.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", new { message = e.Message });
            }
            //return RedirectToAction("Exiting",new { votes=exitingModel.GetSelectedVotesPosts()});
            return RedirectToAction(nameof(Index)); 
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Error(string message)
        {
             await HttpContext.SignOutAsync("Identity.Application");
            ViewBag.Message = message ?? "You Have Already Voted";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Done()
        {
            string user = httpContext.HttpContext.User.Identity.Name.ToString();
            var student =await userManager.FindByNameAsync(user);
            Election.AlreadyVoted.Add(new AlreadyVoted()
            {
                Student = student.Id
            });
            try
            {
                await Election.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Exiting(List<SelectedVotesPostModel> votes)
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