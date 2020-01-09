using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models.DbContexts
{
    public class ElectionDbContext:DbContext
    {
        public ElectionDbContext(DbContextOptions<ElectionDbContext> options):base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<AlreadyVoted> AlreadyVoted { get; set; }
        public DbSet<ElectionState> ElectionState { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Votes>(options =>
            {
                options.HasKey(o => new { o.CategoryId, o.CandidateId });
            });
            builder.Entity<AlreadyVoted>(options =>
            {
                options.HasKey(o => new { o.Student, o.ElectionId });
            });

        }
    }
}
