using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveMotion.Core.Entities
{
    public class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PresentationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Stream { get; set; }

        [ForeignKey("PresentationId")]
        public Presentation Presentation { get; set; }
    }
}
