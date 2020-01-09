using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models
{
    [Table(name:"Election")]
    public class ElectionState
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date),Display(Name ="Date Closed")]
        public DateTime DateClosed { get; set; }
        [DataType(DataType.Text)]
        public bool Ongoing { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
