using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models
{
    public class Votes
    {
        public int CategoryId { get; set; }
        public int CandidateId { get; set; }
        public Category Category { get; set; }
        public Candidate Candidate { get; set; }
        public int VoteCount { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
