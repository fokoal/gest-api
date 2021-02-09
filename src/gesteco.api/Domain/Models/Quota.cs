using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Quota {

        [Key]
        public long IdQuota { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [Required]
        public long Quantite_Disponible { get; set; } 
 
        [Required]
        public long Quantite_Commerce { get; set; } 
        [Required]
        public long Quantite_Initiale { get; set; } 

        [Required]
        public string IdCivique { get; set; }

        [ForeignKey("IdCivique")]
        public Adresse Adresse { get; set; }

        public IEnumerable<Historique_Quota> Historiques { get; set; }



    }
}
