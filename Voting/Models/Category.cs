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
        public int Id { get; set; }
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [Timestamp]
        public byte[] TimeSamp { get; set; }
    }
}
