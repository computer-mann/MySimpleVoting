using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Models.DbContexts;

namespace Voting.Controllers
{
    [Authorize]
    public class VotingController : Controller
    {
        public ElectionDbContext  Election { get; set; }
        public VotingController(ElectionDbContext electionDbContext)
        {
            Election = electionDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}