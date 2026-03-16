public class ProductFilterViewModel
{
    public string? SearchTerm { get; set; }

    public int? CategoryId { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
}