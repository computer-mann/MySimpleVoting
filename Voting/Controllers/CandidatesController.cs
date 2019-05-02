using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.Infrastructure.Interfaces;
using Voting.Models;
using Voting.Models.DbContexts;
using Voting.Models.ViewModels;

namespace Voting.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class CandidatesController : Controller
    {
        private readonly ElectionDbContext dbContext;
        private readonly IPictureUpload pictureUpload;
        public CandidatesController(ElectionDbContext electionDb, IPictureUpload pictureUploads)
        {
            dbContext = electionDb;
            pictureUpload = pictureUploads;
        }

        [HttpGet]
        public IActionResult List()
        {
            var can = dbContext.Candidates.OrderBy(op => op.CandidateName).ToList();
            return View(can);
        }
        [HttpGet]
        public IActionResult CreateCandidate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromForm]AddCandidateViewModel viewModel)
        {
            var pic =await pictureUpload.UploadAsync(viewModel.Photo);
            var can = new Candidate()
            {
                CandidateName = viewModel.CandidateName,
                Photo = pic
            };
            dbContext.Candidates.Add(can);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult EditCandidate(int? canId)
        {
            if (canId == null) return BadRequest();
            var can = dbContext.Candidates.Find(canId);
            if (can == null) return NotFound();
            var editcan = new EditCandidateViewModel() { CandidateName = can.CandidateName };
            ViewBag.CanId = canId;
            return View(editcan);
        }
        [HttpPost]
        public async Task<IActionResult> EditCandidate([FromForm]EditCandidateViewModel candidate,int canId)
        {
            var can =await dbContext.Candidates.FindAsync(canId);
            can.CandidateName = candidate.CandidateName;
            if (candidate.Photo != null)
            {
            var pic =await pictureUpload.UploadAsync(candidate.Photo);
            can.Photo = pic;        
            }
            dbContext.Candidates.Update(can);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult DeleteCandidate(int? canId)
        {
            if (canId == null) return RedirectToAction("List");
            var can = dbContext.Candidates.Find(canId);
            if (can == null) return NotFound();
            return View(can);
        }
        [HttpPost]
        public IActionResult DeleteCandidate(Candidate cannId)
        {
            if (cannId == null) return NotFound();

            var can = dbContext.Candidates.Find(cannId.CanId);
            if (can == null) return RedirectToAction("List");

            dbContext.Candidates.Remove(can);
            dbContext.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Details(int? canId)
        {
            if (canId == null) return RedirectToAction(nameof(List));

            var can = dbContext.Candidates.Find(canId);
            return View(can);
        }
    }
}