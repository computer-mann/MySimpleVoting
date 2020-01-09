using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Models.DbContexts;
using Voting.Models.ViewModels;

namespace Voting.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]/[action]")]
    public class ResultController : Controller
    {
        private readonly ElectionDbContext el;

        public ResultController(ElectionDbContext electionDbContext)
        {
            el = electionDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cats = el.Categories.ToList();
            return View(cats);
        }
        [HttpGet]
        public IActionResult ResultDetails(int catId)
        {
            var catCanVotes = el.Votes.Where(p => p.CategoryId == catId).ToList();
            List<CandidateVoteCount> candidateVote = new List<CandidateVoteCount>();
            foreach(var can in catCanVotes)
            {
                var candi = el.Candidates.Find(can.CandidateId);
                candidateVote.Add(new CandidateVoteCount()
                {
                    CandidateName = candi.CandidateName,
                    Photo=candi.Photo,
                    NumberOfVotes = can.VoteCount
                });
            }
            ViewBag.CatName = el.Categories.Find(catId).CategoryName;
            return View(candidateVote);
        }
    }
}