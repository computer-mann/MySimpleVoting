using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models.DbContexts;

namespace Voting.Models.Data
{
    public class SeedData
    {
        //private readonly ElectionDbContext electionDb;
        //public SeedData(ElectionDbContext electionDbs)
        //{
        //    electionDb = electionDbs;
        //}
        public static async Task  InitializeVoteParams(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ElectionDbContext>();
            var cans = dbContext.Candidates.Include(op => op.Category).ToList();
            //var cats = dbContext.Categories.ToList();
            List<Votes> votes = new List<Votes>();
            foreach(var cand in cans)
            {
                var cat = cand.Category;
                votes.Add(new Votes()
                {
                    Candidate = cand,
                    Category = cat,
                    VoteCount = 0
                });
            }
                await dbContext.Votes.AddRangeAsync(votes);
                await dbContext.SaveChangesAsync();
        }
    }
}
