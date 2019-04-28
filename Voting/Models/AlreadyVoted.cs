using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Voting.Areas.Identity.Models;

namespace Voting.Models
{
    public class AlreadyVoted
    {
        [Key]
        public int Id { get; set; }
        public Student  Student { get; set; }
        public bool Voted { get; set; }
        
    }
}
