using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.ContactMessage;

namespace SentinelGear.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето 'Име' е задължително.")]
        [MaxLength(NameMaxLength, ErrorMessage = "Полето 'Име' не може да надвишава {1} символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Имейл' е задължително.")]
        [EmailAddress]
        [MaxLength(EmailMaxLength, ErrorMessage = "Полето 'Имейл' не може да надвишава {1} символа.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Тема' е задължително.")]
        [MaxLength(SubjectMaxLength, ErrorMessage = "Полето 'Тема' не може да надвишава {1} символа.")]
        [Display(Name = "Тема")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Съобщение' е задължително.")]
        [MaxLength(MessageMaxLength, ErrorMessage = "Полето 'Съобщение' не може да надвишава {1} символа.")]
        [Display(Name = "Съобщение")]
        public string Message { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}