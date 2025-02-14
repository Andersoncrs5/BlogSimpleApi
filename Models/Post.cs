using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSimpleApi.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "The title field is required.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "The title must be between 5 and 150 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "The content field is required.")]
        [MaxLength(3000, ErrorMessage = "The content must be at least 3000 characters long.")]
        [MinLength(50, ErrorMessage = "Size minimum of 50" )]
        public string Content { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 5, ErrorMessage = "The EmailUser must be between 5 and 150 characters.")]
        public string EmailUser { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user ID is required.")]
        public long UserId { get; set; }
        public bool IsEdited { get; set; } = false;
        public bool IsActived { get; set; } = false;
        public bool IsBlock { get; set; } = false;
        public bool IsBlockByUser { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
