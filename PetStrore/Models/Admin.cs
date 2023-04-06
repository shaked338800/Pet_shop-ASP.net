using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetStrore.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter User Name !")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Please enter Password !")]
        public string? Password { get; set; }
        
    }
}
