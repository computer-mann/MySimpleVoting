using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models;
using Voting.Models.DbContexts;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Voting.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]/[action]")]
    public class ElectionController:Controller
    {
        private readonly ElectionDbContext dbContext;
        private readonly IHostingEnvironment hosting;
        public ElectionController(ElectionDbContext electionDb,IHostingEnvironment hostingEnvironment)
        {
            dbContext = electionDb;
            hosting = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult List()
        {
            var all = dbContext.ElectionState.ToList();
            return View(all);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ElectionState election)
        {
            if (!ModelState.IsValid) return View(election);
            dbContext.ElectionState.Add(election);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(Guid guid)
        {
            var res= dbContext.ElectionState.Find(guid);
            return View(res);
        }

        [HttpPost]
        public IActionResult Edit(ElectionState election)
        {
            if (!ModelState.IsValid) return View(election);
            var res = dbContext.ElectionState.Find(election.Id);
            res.Description = election.Description;
            res.DateClosed = election.DateClosed;
            res.Ongoing = election.Ongoing;
            dbContext.ElectionState.Update(res);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception) { }
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public IActionResult Details(Guid guid)
        {
            var res= dbContext.ElectionState.Find(guid);
            return View(res);
        }
        [HttpGet]
        public IActionResult AddCatToElection(Guid guid)
        {
            ViewBag.Guid = guid;
            ViewBag.Description = dbContext.ElectionState.Find(guid).Description;
            var cats = dbContext.Categories.Where(p => p.Election.Id == null).ToList();
            return View(cats);
        }
        [HttpGet]
        public async Task<IActionResult> AddCatToElectionPost(int catId,Guid guid)
        {
            var cat = dbContext.Categories.Find(catId);
            var ele = dbContext.ElectionState.Find(guid);
            cat.Election = ele;
            dbContext.Categories.Update(cat);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { }
            return RedirectToAction(nameof(Details), new { guid });
        }
        [HttpGet]
        public IActionResult Report(Guid guid)
        {
            var report = dbContext.ElectionState.Find(guid);
            ViewBag.Guid = guid;
            return View(report);
        }
        [HttpPost]
        public async Task<IActionResult> ExcelReport(Guid guid)
        {
            if (string.IsNullOrEmpty(guid.ToString())) return RedirectToAction(nameof(Report), new { guid });
            var election = await dbContext.ElectionState.FindAsync(guid);
            var cats = dbContext.Categories.Where(p => p.Election.Id == guid).ToList();
            var candidates = dbContext.Candidates.ToList();
            List<Candidate> candidateList = new List<Candidate>();
            List<Votes> voters = new List<Votes>();
            foreach(var cat in cats)
            {
                foreach(var can in candidates)
                {
                    if (can.Category == null) continue;
                    if (cat.CatId == can.Category.CatId)
                    {
                        candidateList.Add(can);
                    }
                }
            }
            var votes = dbContext.Votes.ToList();
            foreach(var can in candidateList)
            {
                foreach(var v in votes)
                {
                    if (can.CanId == v.CandidateId)
                    {
                        voters.Add(v);
                    }
                }
            }
            string sWebRootFolder = hosting.WebRootPath;
            string sFileName = @"report.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            MemoryStream memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Report");
                excelSheet.DefaultColumnWidth = 20;

                IRow row0 = excelSheet.CreateRow(1);
                row0.HeightInPoints = 23.1f;
                var headingcell = row0.CreateCell(3);
                var fontheading = workbook.CreateFont();
                fontheading.Boldweight = 18;
                fontheading.FontHeightInPoints = 18;
                var styleheading = workbook.CreateCellStyle();
                styleheading.VerticalAlignment = VerticalAlignment.Center;
                styleheading.Alignment = HorizontalAlignment.Center;
                styleheading.SetFont(fontheading);
                headingcell.SetCellValue($"Election Report - {DateTime.Now.ToShortDateString()}");
                headingcell.CellStyle = styleheading;
                


                IRow row = excelSheet.CreateRow(4);
                row.HeightInPoints = 20.1f;
                row.CreateCell(0).SetCellValue("Election Code ");
                row.CreateCell(1).SetCellValue("Description");
                row.CreateCell(2).SetCellValue("Category");
                row.CreateCell(3).SetCellValue("Candidate Name");
                row.CreateCell(4).SetCellValue("Votes Obtained");
                //row.CreateCell(5).SetCellValue("Position");



                int i = 5;
                foreach (var can in candidateList)
                {

                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(election.Id.ToString().Substring(0, 6));
                    row.CreateCell(1).SetCellValue(election.Description);
                    var catName = can.Category.CategoryName;
                    row.CreateCell(2).SetCellValue(catName);
                    row.CreateCell(3).SetCellValue(can.CandidateName);
                    var position = voters.Find(p => p.CandidateId == can.CanId);
                    row.CreateCell(4).SetCellValue(position.VoteCount);
                    //   row.CreateCell(5).SetCellValue(votes.IndexOf(position) + 1);
                    //if(!catName.Equals(can.Category.CategoryName))
                    //{
                    //    row = excelSheet.CreateRow(i);
                    //}
                    i++;
                }





                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}
