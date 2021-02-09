using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Provenance {

        [Key]
        public long IdProvenance { get; set; }

        public string IdCivique { get; set; } 

        [Required]
        [MaxLength(250)]
        public string Adresse { get; set; } 

        [Required]
        public long Quantite_Disponible { get; set; } 


        [Required]
        public long Quantite_Initiale { get; set; } 

        public IEnumerable<Visite> Visites { get; set; }

    }
}
