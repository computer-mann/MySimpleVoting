using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        [MaxLength(60),Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        public List<Candidate> Candidates { get; set; }
        [Timestamp]
        public byte[] TimeSamp { get; set; }
        public ElectionState Election { get; set; }
    }
}
