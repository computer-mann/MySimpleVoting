﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models.ViewModels
{
    public class CandidateViewModel
    {
        [Display(Name ="Candidate")]
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
