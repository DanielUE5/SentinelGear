using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.CategoryFormViewModel;

namespace SentinelGear.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на категорията е задължително.")]
        [MaxLength(NameMaxLength, ErrorMessage = "Името може да е максимум 100 символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;
    }
}