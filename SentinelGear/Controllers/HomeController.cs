using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelGear.Data;
using SentinelGear.Models;
using SentinelGear.ViewModels;
using System.Diagnostics;

namespace SentinelGear.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly SentinelGearDbContext dbContext;

        public HomeController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
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
