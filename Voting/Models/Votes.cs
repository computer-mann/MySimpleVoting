using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models
{
    public class Votes
    {
        [Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        public Candidate Candidate { get; set; }
        public int VoteCount { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
