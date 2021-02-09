using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Historique_Quota {
       
        [Key]
        public long IdHistorique_Quota { get; set; }

        public DateTime DateHistorique { get; set; } = DateTime.Now;
      
        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [Required]
        public long Quantite_Utilisee { get; set; }

        [Required]
        public long Quantite_Initiale { get; set; }

        [Required]
        public string IdCivique { get; set; }
     
        [Required]
        public long IdQuota { get; set; } 

        [ForeignKey("IdQuota")]
        public Quota Quota { get; set; }
    }
}
