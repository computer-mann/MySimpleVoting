using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models.DbContexts;

namespace Voting.Controllers.Components
{
    [ViewComponent(Name = "CandidateInCategory")]
    public class CandidateInCategory:ViewComponent
    {
        private readonly ElectionDbContext electionDb;
        public CandidateInCategory(ElectionDbContext dbContext)
        {
            electionDb = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? catId)
        {
            var cans =electionDb.Candidates.Where(p => p.Category.CatId == catId).ToList();
            return View(cans);
        }
    }
}
