using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models.DbContexts;

namespace Voting.Controllers.Components
{
    [ViewComponent(Name = "CategoriesInElection")]
    public class CategoriesInElection: ViewComponent
    {
        private readonly ElectionDbContext electionDb;
        public CategoriesInElection(ElectionDbContext dbContext)
        {
            electionDb = dbContext;
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync(Guid guid)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var cats = electionDb.Categories.Where(p => p.Election.Id == guid).ToList();
            return View(cats);
        }
    }
}
