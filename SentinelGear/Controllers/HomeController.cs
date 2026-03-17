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

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacts(ContactMessage model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Моля, попълнете всички задължителни полета правилно.";
                return View(model);
            }

            try
            {
                dbContext.ContactMessages.Add(model);
                await dbContext.SaveChangesAsync();

                TempData["Success"] = "Благодарим Ви! Вашето съобщение беше изпратено успешно.";
                return RedirectToAction(nameof(Contacts));
            }
            catch
            {
                TempData["Error"] = "Възникна грешка при изпращането. Моля, опитайте отново.";
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
