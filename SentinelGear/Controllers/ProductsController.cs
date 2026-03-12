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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await dbContext.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Category)
                .ToListAsync();

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