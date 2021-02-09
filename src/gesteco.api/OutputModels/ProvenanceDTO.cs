using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class ProvenanceDTO {

      
        public long IdProvenance { get; set; }  

        public string IdCivique { get; set; }
        [Required]
        [MaxLength(250)]

        public string Adresse { get; set; } 
 
        public long Quantite_Disponible { get; set; } 
        
        public long Quantite_Initiale { get; set; }  

        public IEnumerable<VisiteDTO> Visites { get; set; }
        public IEnumerable<Matiere_VisiteDTO> Matieres { get; set; }
    }
}
