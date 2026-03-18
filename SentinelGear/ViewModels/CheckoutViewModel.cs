using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.CheckoutViewModel;

namespace SentinelGear.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Име")]
        [MaxLength(FirstNameMaxLength, ErrorMessage = "Името не може да бъде по-дълго от 50 символа.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(LastNameMaxLength, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа.")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [Display(Name = "Имейл")]
        [MaxLength(EmailMaxLength, ErrorMessage = "Имейл адресът не може да бъде по-дълъг от 100 символа.")]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(PhoneNumberPattern, ErrorMessage = "Невалиден телефонен номер")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Адрес")]
        [MaxLength(AddressMaxLength, ErrorMessage = "Адресът не може да бъде по-дълъг от 100 символа.")]
        public string Address { get; set; } = null!;
    }
}