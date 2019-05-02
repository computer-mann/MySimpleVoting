using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models.ViewModels
{
    public class EditCandidateViewModel
    {
        [MaxLength(20), DataType(DataType.Text), Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }
        [Display(Name = "Picture"), DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}
