using System.ComponentModel.DataAnnotations;

namespace BlogSimpleApi.Models;

public class Comment
{
    [Key]
    public long Id { get; set; }

    [Required(ErrorMessage = "The content field is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The content must be between 2 and 100 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The content field is required.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "The content must be between 5 and 500 characters.")]
    public string Content { get; set; } = string.Empty;

    [StringLength(150, MinimumLength = 5, ErrorMessage = "The EmailUser must be between 5 and 150 characters.")]
    public string EmailUser { get; set; } = string.Empty;

    [Required(ErrorMessage = "The user ID is required.")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "The post ID is required.")]
    public long PostId { get; set; }

    public bool IsEdited { get; set; } = false;
    public bool IsActived { get; set; } = false;

    public bool IsBlock { get; set; } = false;
    public bool IsBlockByPost { get; set; } = false;
    public bool IsBlockByUser { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
