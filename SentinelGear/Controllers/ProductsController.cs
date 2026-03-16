using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;
using SentinelGear.Helpers;

namespace SentinelGear.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly SentinelGearDbContext dbContext;

        public ProductsController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index(ProductFilterViewModel filter)
        {
            IQueryable<Product> productsQuery = dbContext.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(filter.SearchTerm) ||
                    p.Description.Contains(filter.SearchTerm) ||
                    (p.Manufacturer != null && p.Manufacturer.Contains(filter.SearchTerm)));
            }

            if (filter.CategoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
            {
                productsQuery = productsQuery.Where(p => p.Manufacturer == filter.Manufacturer);
            }

            // Added a small tolerance to the max price to account for rounding differences in currency conversion
            decimal tolerance = 0.01m;
            decimal? minPriceBgn = null;
            decimal? maxPriceBgn = null;

            if (filter.MinPrice.HasValue)
            {
                minPriceBgn = CurrencyConverter.ConvertEurToBgn(filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                maxPriceBgn = CurrencyConverter.ConvertEurToBgn(filter.MaxPrice.Value) + tolerance;
            }

            if (minPriceBgn.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPriceBgn.Value);
            }

            if (maxPriceBgn.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maxPriceBgn.Value);
            }

            List<Product> products = await productsQuery.ToListAsync();

            List<SelectListItem> categories = await dbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            List<SelectListItem> manufacturers = await dbContext.Products
                .Where(p => p.Manufacturer != null && p.Manufacturer != "")
                .Select(p => p.Manufacturer!)
                .Distinct()
                .OrderBy(m => m)
                .Select(m => new SelectListItem
                {
                    Value = m,
                    Text = m
                })
                .ToListAsync();

            ProductCatalogViewModel viewModel = new ProductCatalogViewModel
            {
                Products = products,
                Filter = filter,
                Categories = categories,
                Manufacturers = manufacturers
            };

            return View(viewModel);
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