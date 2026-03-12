using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;

namespace SentinelGear.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly SentinelGearDbContext dbContext;

        public ProductsController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string? searchTerm, int? categoryId)
        {
            IQueryable<Product> productsQuery = dbContext.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string normalizedSearch = searchTerm.Trim().ToLower();

                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(normalizedSearch));
            }

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            IEnumerable<Product> products = await productsQuery
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Category!.Name)
                .ToListAsync();

            ViewBag.Categories = await dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.SelectedCategory = categoryId;
            ViewBag.SearchTerm = searchTerm;

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product? product = await dbContext.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}