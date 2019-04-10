using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name ="Category")]
        public string CategoryName { get; set; }

    }
}
