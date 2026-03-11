using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;
using SentinelGear.ViewModels;
using System.Diagnostics;

namespace SentinelGear.Controllers
{
    public class HomeController : BaseController
    {
        // The HomeController does not currently interact with the database,
        // but we inject the SentinelGearDbContext here for potential future use, such as displaying featured products or categories on the home page.
        private readonly SentinelGearDbContext dbContext;

        public HomeController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View(); 
        }

        public async Task<IActionResult> Contacts() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
