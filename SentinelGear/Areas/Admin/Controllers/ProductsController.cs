using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Helpers;
using SentinelGear.Models;
using SentinelGear.ViewModels;

namespace SentinelGear.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly SentinelGearDbContext dbContext;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(SentinelGearDbContext context, IWebHostEnvironment environment)
        {
            dbContext = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await dbContext.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductFormViewModel model = new()
            {
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (model.ImageFile != null && !ImageUploader.IsValidImage(model.ImageFile))
            {
                ModelState.AddModelError(nameof(model.ImageFile), "Моля, качете валидно изображение (.jpg, .jpeg, .png, .webp) до 5MB.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = GetCategories();
                return View(model);
            }

            string? imageUrl = null;

            if (model.ImageFile != null)
            {
                imageUrl = await ImageUploader.SaveImageAsync(model.ImageFile, _environment.WebRootPath);
            }

            Product product = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                Manufacturer = model.Manufacturer,
                CategoryId = model.CategoryId,
                ImageUrl = imageUrl
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Продуктът беше добавен успешно.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product? product = await dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductFormViewModel model = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Manufacturer = product.Manufacturer,
                CategoryId = product.CategoryId,
                ExistingImageUrl = product.ImageUrl,
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            Product? product = await dbContext.Products.FindAsync(model.Id);

            if (product == null)
            {
                return NotFound();
            }

            if (model.ImageFile != null && !ImageUploader.IsValidImage(model.ImageFile))
            {
                ModelState.AddModelError(nameof(model.ImageFile), "Моля, качете валидно изображение (.jpg, .jpeg, .png, .webp) до 5MB.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = GetCategories();
                model.ExistingImageUrl = product.ImageUrl;
                return View(model);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.Manufacturer = model.Manufacturer;
            product.CategoryId = model.CategoryId;

            if (model.ImageFile != null)
            {
                product.ImageUrl = await ImageUploader.SaveImageAsync(model.ImageFile, _environment.WebRootPath);
            }

            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Продуктът беше редактиран успешно.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product? product = await dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.IsDeleted = true;
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Продуктът беше изтрит успешно.";
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetCategories()
        {
            return dbContext.Categories
                .AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
    }
}