using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Areas.Identity.Models.ViewModels
{
    public class AdminLoginViewModel
    {
        public string AdminId { get; set; }

        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
