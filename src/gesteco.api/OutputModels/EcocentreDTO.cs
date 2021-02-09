using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class EcocentreDTO {

       
        public long IdEcocentre { get; set; }

        [Required]
        [MaxLength(300)]
        public string Nom { get; set; }

       
        [MaxLength(250)]
        public string Adresse { get; set; }

       
        [MaxLength(10)]
        public string Rue { get; set; }

      
        [MaxLength(50)]
        public string Ville { get; set; }

       
        [MaxLength(6)]
        public string Codepostal { get; set; }

        public IEnumerable<VisiteDTO> Visites { get; set; }

        public IEnumerable<Ecocentre_MatiereDTO> Matieres { get; set; }
    }
}
