using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;

namespace SentinelGear.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly SentinelGearDbContext dbContext;

        public CategoriesController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await dbContext.Categories
                .Include(c => c.Products.Where(p => !p.IsDeleted))
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(categories);
        }
    }
}