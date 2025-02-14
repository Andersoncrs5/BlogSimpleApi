using System.ComponentModel.DataAnnotations;

namespace BlogSimpleApi.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "The name field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The content must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "The email must be between 10 and 150 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password field is required.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "The password must be between 5 and 250 characters.")]
        public string Password { get; set; } = string.Empty;
        public bool IsAdm { get; set; } = false;
        public bool IsBlock { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
