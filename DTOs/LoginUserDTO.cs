using System.ComponentModel.DataAnnotations;

namespace BlogSimpleApi.DTOs
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "The email must be between 10 and 150 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password field is required.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "The password must be between 5 and 250 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
