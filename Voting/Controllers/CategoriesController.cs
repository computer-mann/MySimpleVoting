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
    public class CategoriesController : Controller
    {
        private readonly ElectionDbContext electionDb;
        public CategoriesController(ElectionDbContext electionDbContext)
        {
            electionDb = electionDbContext;
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([Bind("CategoryName")]Category category)
        {
            if (ModelState.IsValid)
            {
                await electionDb.Categories.AddAsync(new Category() { CategoryName = category.CategoryName });
                await electionDb.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult List()
        {
            var cats = electionDb.Categories.ToList();
            return View(cats);
        }
        [HttpGet()]
        public IActionResult EditCategory(int catId)
        {
            var cat = electionDb.Categories.Single(b => b.CatId == catId);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category, int catId)
        {
            var can = electionDb.Categories.Find(catId);
            can.CategoryName = category.CategoryName;
            electionDb.Categories.Update(can);
            electionDb.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public ViewResult Details(int catId)
        {
            var cat = electionDb.Categories.Single(b => b.CatId == catId);
            ViewBag.NoOfCandidates = electionDb.Candidates.Where(p => p.Category.CatId == cat.CatId).Count();
            return View(cat);
        }
        [HttpGet]
        public IActionResult AddCandidatesToCategory(int? catId)
        {
            if (catId == null) return NotFound();
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var can = electionDb.Candidates.Where(p => p.Category.CatId == null).ToList();
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            ViewBag.CName = electionDb.Categories.Find(catId).CategoryName;
            ViewBag.CatId = catId.Value;
            return View(can);
        }
        [HttpGet]
        public IActionResult Confirm(int? canId,int catId)
        {
            if (canId == null) return NotFound();
            ViewBag.CatId = catId;
            var can = electionDb.Candidates.Find(canId.Value);
            return View(can);
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(int? catId,Candidate candidate)
        {
            var cand = electionDb.Candidates.Find(candidate.CanId);
            if (catId == null) return NotFound();
            var cat = electionDb.Categories.Find(catId.Value);
            if (cat == null) return NotFound();
            cand.Category = cat;
            electionDb.Candidates.Update(cand);
           await electionDb.Votes.AddAsync(new Votes()
            {
               Candidate=cand,Category=cat,VoteCount=0
            });
            await electionDb.SaveChangesAsync();
            return RedirectToAction(nameof(Details),new { catId=catId.Value});
        }
        [HttpGet]
        public IActionResult Delete(int catId)
        {
            var cat = electionDb.Categories.Find(catId);
            electionDb.Categories.Remove(cat);
            electionDb.SaveChanges();
            return RedirectToAction(nameof(List));
        }
    }
}