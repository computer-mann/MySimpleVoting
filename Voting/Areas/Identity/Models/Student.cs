using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Areas.Identity.Models
{
    public class Student:IdentityUser
    {
        public string StudentId { get; set; }
        [MaxLength(35)]
        public string FirstName { get; set; }
        [MaxLength(35)]
        public string LastName { get; set; }
        [MaxLength(35)]
        public string OtherNames { get; set; }
        public string Image { get; set; }
    }
}
