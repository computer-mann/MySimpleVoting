using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Voting.Areas.Identity.Models;

namespace Voting.Models
{
    public class AlreadyVoted
    {
        
        public string  Student { get; set; }
        public Guid ElectionId { get; set; }

    }
}
