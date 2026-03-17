using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SentinelGear.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(100)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Описанието е задължително.")]
        [StringLength(1000)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Цената е задължителна.")]
        [Range(typeof(decimal), "0.01", "999999.99", ErrorMessage = "Цената трябва да бъде по-голяма от 0.")]
        [Display(Name = "Цена (лв.)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Количеството е задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Количеството не може да е отрицателно.")]
        [Display(Name = "Наличност")]
        public int StockQuantity { get; set; }

        [StringLength(100)]
        [Display(Name = "Производител")]
        public string? Manufacturer { get; set; }

        [Required(ErrorMessage = "Категорията е задължителна.")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}