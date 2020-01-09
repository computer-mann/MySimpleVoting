using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                //new Student(){Email="d@d.com",UserName="dadjeifrempah",StudentId="20171717"},
                //new Student(){Email="o@q.com",UserName="ogyimah",StudentId="20191756"},
                //new Student(){Email="p@h.com",UserName="phnunoo",StudentId="20426845"},
                //new Student(){Email="v@n.com",UserName="vnunoo",StudentId="20200524"},
                //new Student(){Email="w@q.com",UserName="wquarshie",StudentId="20131226"},
                  new Student(){Email="a@a.com",UserName="gifty1",StudentId="20181717"},
                    new Student(){Email="b@d.com",UserName="joseph3",StudentId="20171709"},
                      new Student(){Email="d@das.com",UserName="cynthia67",StudentId="31871717"},
                        new Student(){Email="d@push.com",UserName="veraashy2",StudentId="20171717"},
                          new Student(){Email="d@puxes.com",UserName="thedorah67",StudentId="20171717"}
            };
            foreach(var student in students)
            {
                await userManager.CreateAsync(student,"2kboyka");
            }
        }
        public async static Task InitializeStudentRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = new IdentityRole("Student");
            var userManager = serviceProvider.GetRequiredService<UserManager<Student>>();
            var students = new List<Student>()
            {
                //new Student(){Email="d@d.com",UserName="dadjeifrempah",StudentId="20171717"},
                //new Student(){Email="o@q.com",UserName="ogyimah",StudentId="20191756"},
                //new Student(){Email="p@h.com",UserName="phnunoo",StudentId="20426845"},
                //new Student(){Email="v@n.com",UserName="vnunoo",StudentId="20200524"},
                //new Student(){Email="w@q.com",UserName="wquarshie",StudentId="20131226"},
                 new Student(){Email="a@a.com",UserName="gifty1",StudentId="20181717"},
                    new Student(){Email="b@d.com",UserName="joseph3",StudentId="20171709"},
                      new Student(){Email="d@das.com",UserName="cynthia67",StudentId="31871717"},
                        new Student(){Email="d@push.com",UserName="veraashy2",StudentId="20171717"},
                          new Student(){Email="d@puxes.com",UserName="thedorah67",StudentId="20171717"}
            };
            //await roleManager.CreateAsync(role);
            foreach (var stu in students)
            {
                var user =await userManager.FindByEmailAsync(stu.Email);
                await userManager.AddToRoleAsync(user, "Student");
            }
        }
        public static async Task InitializeAdmins(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = new IdentityRole("Admin");
            var userManager = provider.GetRequiredService<UserManager<Student>>();
            var student = new Student()
            {
                UserName = "admin",
                StudentId = "1",
                Email = "admin@gmail.com"
            };
            await roleManager.CreateAsync(role);
            await userManager.CreateAsync(student,"admin");
            await userManager.AddToRoleAsync(student, "Admin");
        }
    }
}
