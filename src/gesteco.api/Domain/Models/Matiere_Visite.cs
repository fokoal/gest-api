using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Matiere_Visite {

        [Key]
        public long IdMatiere_Visite { get; set; }

        public long IdVisite { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } 

        public bool Comptable { get; set; } 

        [ForeignKey("IdVisite")]
        public Visite Visite { get; set; }
    }
}
