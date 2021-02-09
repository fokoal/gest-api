using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Matiere {

        [Key]
        public long IdMatiere { get; set; } 

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public bool Comptable { get; set; } 
    }
}
