using System.ComponentModel.DataAnnotations;

namespace SentinelGear.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Име")]
        [StringLength(50, ErrorMessage = "Името не може да бъде по-дълго от 50 символа.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа.")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [Display(Name = "Имейл")]
        [StringLength(100, ErrorMessage = "Имейл адресът не може да бъде по-дълъг от 100 символа.")]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "Адресът не може да бъде по-дълъг от 100 символа.")]
        public string Address { get; set; } = null!;
    }
}