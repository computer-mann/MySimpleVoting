using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Voting.Areas.Identity.Models;
using Voting.Models;

namespace Voting.Controllers
{
    [Authorize]
   // [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly SignInManager<Student> signIn;
        private readonly IMemoryCache cache;
        public HomeController(SignInManager<Student> inManager, IMemoryCache caches)
        {
            signIn = inManager;
            cache = caches;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
           // cache.Dispose();
            signIn.SignOutAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
