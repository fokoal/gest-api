using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Adresse {

        [Key]
        public string IdCivique { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nom { get; set; } 
    }
}
