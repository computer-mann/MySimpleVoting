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
    [Authorize(Roles ="Admin")]
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
        public IActionResult EditCategory(int id)
        {
            var cat = electionDb.Categories.Single(b => b.Id == id);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category,int id)
        {
            var can = electionDb.Categories.Find(id);
            can.CategoryName = category.CategoryName;
            electionDb.Categories.Update(can);
            electionDb.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public ViewResult Details(int id)
        {
            var can = electionDb.Categories.Single(b => b.Id == id);
            ViewBag.NoOfCandidates = electionDb.Candidates.Where(p => p.Category.Id == can.Id).Count();
            return View(can);
        }
        [HttpGet]
        public async Task<IActionResult> AddCandidatesToCategory()
        {
            var can = electionDb.Candidates.Where(p => p.Category.Id == null).ToList();
            return View(can);
        }
        [HttpPost]
        public async Task<IActionResult> AddCandidatesToCategory(int? id,IEnumerable<Candidate> candidates)
        {
            if (id == null) return NotFound();
            foreach(var can in candidates)
            {
                can.Category.Id = id.Value;
            }
            electionDb.Candidates.UpdateRange(candidates);
            await electionDb.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }
    }
}