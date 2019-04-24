using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Models;
using Voting.Models.DbContexts;

namespace Voting.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class CandidatesController : Controller
    {
        private readonly ElectionDbContext dbContext;
        public CandidatesController(ElectionDbContext electionDb)
        {
            dbContext = electionDb;
        }

        [HttpGet]
        public IActionResult List()
        {
            var can = dbContext.Candidates.ToList();
            return View(can);
        }
        [HttpGet]
        public IActionResult CreateCandidate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCandidate([Bind("CandidateName")]Candidate candidate)
        {
            var can = dbContext.Candidates.Add(candidate);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult EditCandidate(int? id)
        {
            if (id == null) return NotFound();
            var can = dbContext.Candidates.Find(id);
            if (can == null) return NotFound();

            return View(can);
        }
        [HttpPost]
        public async Task<IActionResult> EditCandidate(Candidate candidate,int id)
        {
            var can =await dbContext.Candidates.FindAsync(id);
            can.CandidateName = candidate.CandidateName;
            can.Year = candidate.Year;
         //   can.Photo = candidate.Photo;
            dbContext.Candidates.Update(can);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult DeleteCandidate(int? id)
        {
            if (id == null) return RedirectToAction("List");
            var can = dbContext.Candidates.Find(id);
            if (can == null) return NotFound();
            return View(can);
        }
        [HttpPost]
        public IActionResult DeleteCandidate(Candidate can)
        {            
            dbContext.Candidates.Remove(can);
            dbContext.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(List));

            var can = dbContext.Candidates.Find(id);
            return View(can);
        }
    }
}