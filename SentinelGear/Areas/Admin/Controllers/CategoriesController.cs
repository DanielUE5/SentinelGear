using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;
using SentinelGear.ViewModels;

namespace SentinelGear.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly SentinelGearDbContext dbContext;

        public CategoriesController(SentinelGearDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await dbContext.Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormViewModel model)
        {
            if (await dbContext.Categories.AnyAsync(c => c.Name.ToLower() == model.Name.ToLower()))
            {
                ModelState.AddModelError(nameof(model.Name), "Категория с това име вече съществува.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Category category = new()
            {
                Name = model.Name
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Категорията беше добавена успешно.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category? category = await dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryFormViewModel model = new()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryFormViewModel model)
        {
            Category? category = await dbContext.Categories.FindAsync(model.Id);

            if (category == null)
            {
                return NotFound();
            }

            if (await dbContext.Categories.AnyAsync(c => c.Id != model.Id && c.Name.ToLower() == model.Name.ToLower()))
            {
                ModelState.AddModelError(nameof(model.Name), "Категория с това име вече съществува.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            category.Name = model.Name;
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Категорията беше редактирана успешно.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category? category = await dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.Products.Any(p => !p.IsDeleted))
            {
                TempData["Error"] = "Не можеш да изтриеш категория, която има активни продукти.";
                return RedirectToAction(nameof(Index));
            }

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Категорията беше изтрита успешно.";
            return RedirectToAction(nameof(Index));
        }
    }
}