using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Areas.Identity.Models.DbContext
{
    public class StudentDbContext:IdentityDbContext<Student>
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("UsersIdentity");
            builder.Entity<IdentityRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersInTheirRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRolesAndTheirClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

        }

    }
}
