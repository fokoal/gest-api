using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Visite {

        [Key]
        public long IdVisite { get; set; }

        [Required]
        public long IClient { get; set; }

        [Required]
        public long IdProvenance { get; set; }

        [Required]
        public long IdEcocentre { get; set; }

        public string Employe { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        [ForeignKey("IdProvenance")]
        public Provenance Provenance { get; set; }

        [ForeignKey("IClient")]
        public Client Client { get; set; }

        [ForeignKey("IdVisite")]
        public  Transaction Transaction { get; set; }

        [ForeignKey("IdEcocentre")]
        public Ecocentre Ecocentre { get; set; }

        public IEnumerable<Matiere_Visite> Matieres { get; set; }

    }
}
