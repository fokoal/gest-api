using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class ClientDTO {
        
        public long IdClient { get; set; }

        public DateTime DateCreation { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(250)]
        public string Prenom { get; set; } 
     
        public string Immaticulation { get; set; }

        [Required]
        [MaxLength(250)]
        public string Courriel { get; set; }

        public string NomCommerce { get; set; }

        [Required]
        [MaxLength(15)]
        public string Telephone { get; set; } 

        public string IdCivique { get; set; } 

        public IEnumerable<EntrepriseDTO> Entreprises { get; set; }

        public IEnumerable<VisiteDTO> Visites { get; set; }

        public string NomEntreprise
        {
            get
            {
                return  this.Entreprises.Any() ? this.Entreprises.First().Nom : null ;
            }
        }
    }
}
