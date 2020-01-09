using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models.ViewModels
{
    public class CandidateVoteCount
    {
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }
        public string Photo { get; set; }
        [Display(Name = "Number Of Votes")]
        public int NumberOfVotes { get; set; }
    }
}
