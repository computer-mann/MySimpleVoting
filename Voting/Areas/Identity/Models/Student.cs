using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Areas.Identity.Models
{
    public class Student:IdentityUser
    {
        public string StudentId { get; set; }
    }
}
