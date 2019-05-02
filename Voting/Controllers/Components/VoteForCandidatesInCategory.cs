using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models.DbContexts;

namespace Voting.Controllers.Components
{
    [ViewComponent(Name = "VoteForCandidatesInCategory")]
    public class VoteForCandidatesInCategory:ViewComponent
    {
        private readonly ElectionDbContext electionDb;
        public VoteForCandidatesInCategory(ElectionDbContext dbContext)
        {
            electionDb = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? catId)
        {
            var cans = electionDb.Candidates.Where(p => p.Category.CatId == catId).ToList();
            ViewBag.CategoryNameFull = electionDb.Categories.Find(catId.Value).CategoryName;
            ViewBag.CategoryName = electionDb.Categories.Find(catId.Value).CategoryName.Split(' ').Last();
            return View(cans);
        }
    }
}
