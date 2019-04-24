using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [MaxLength(20),DataType(DataType.Text),Display(Name ="Candidate Name")]
        public string CandidateName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }
        [MaxLength(20)]
        public Category Category { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
    }
}
