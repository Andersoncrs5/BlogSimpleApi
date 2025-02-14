using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSimpleApi.Models
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Garante que o banco gera o ID

        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long PostId { get; set; }
    }
}
