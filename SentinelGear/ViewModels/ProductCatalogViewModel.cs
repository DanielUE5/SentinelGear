using Microsoft.AspNetCore.Mvc.Rendering;
using SentinelGear.Models;

public class ProductCatalogViewModel
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();

    public ProductFilterViewModel Filter { get; set; } = new();

    public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

    public IEnumerable<SelectListItem> Manufacturers { get; set; } = new List<SelectListItem>();
}