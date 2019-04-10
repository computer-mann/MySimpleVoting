using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Areas.Identity.Models;

namespace Voting.Areas.Identity.Data
{
    public class SeedData
    {
        public async static Task InitializeStudents(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<Student>>();
            var students = new List<Student>()
            {
                new Student(){Email="d@d.com",UserName="dadjeifrempah",StudentId="20171717"},
                new Student(){Email="o@q.com",UserName="ogyimah",StudentId="20191756"},
                new Student(){Email="p@h.com",UserName="phnunoo",StudentId="20426845"},
                new Student(){Email="v@n.com",UserName="vnunoo",StudentId="20200524"},
                new Student(){Email="w@q.com",UserName="wquarshie",StudentId="20131226"},
            };
            foreach(var student in students)
            {
                await userManager.CreateAsync(student,"2kboyka");
            }
        }
    }
}
