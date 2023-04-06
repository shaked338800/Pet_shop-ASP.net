using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetStrore.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string? Text { get; set; }
        [ForeignKey("Animel")]
        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
