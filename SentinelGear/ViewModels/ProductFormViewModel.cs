using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static SentinelGear.Common.EntityValidation.ProductFormViewModel;

namespace SentinelGear.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [MaxLength(NameMaxLength)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Описанието е задължително.")]
        [MaxLength(DescriptionMaxLength, ErrorMessage = "Описанието не може да бъде по-дълго от 1000 символа.")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Цената е задължителна.")]
        [Range(0.01, 999999.99, ErrorMessage = "Цената трябва да бъде между 0.01 и 999999.99.")]
        [Display(Name = "Цена (лв.)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Количеството е задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Количеството не може да е отрицателно.")]
        [Display(Name = "Наличност")]
        public int StockQuantity { get; set; }

        [MaxLength(ManufacturerMaxLength, ErrorMessage = "Производителят не може да бъде по-дълъг от 100 символа.")]
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