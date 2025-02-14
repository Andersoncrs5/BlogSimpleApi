using System.ComponentModel.DataAnnotations;

namespace BlogSimpleApi.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "The name field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user ID is required.")]
        public long UserId { get; set; }
        public bool IsActived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}