using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetStrore.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId { get; set; }
        [Required(ErrorMessage = "Please enter age !")]
        public int? Age { get; set; }
        [RegularExpression(@"^[a-zA-Z]*$")]
        [Required(ErrorMessage = "Please enter a name !")]
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Please enter a description !")]
        public string? Description { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
