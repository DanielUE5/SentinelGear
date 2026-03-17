using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.ContactMessage;

namespace SentinelGear.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето 'Име' е задължително.")]
        [MaxLength(NameMaxLength)]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Имейл' е задължително.")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Тема' е задължително.")]
        [MaxLength(SubjectMaxLength)]
        [Display(Name = "Тема")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Полето 'Съобщение' е задължително.")]
        [MaxLength(MessageMaxLength)]
        [Display(Name = "Съобщение")]
        public string Message { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}