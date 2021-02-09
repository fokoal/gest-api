using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Client {

        [Key]
        public long IdClient { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Prenom { get; set; } 

      
        [MaxLength(100)]
        public string Immaticulation { get; set; } 

      
        [MaxLength(100)]
        public string Courriel { get; set; } 

        public string NomCommerce { get; set; } 

        [Required]
        [MaxLength(10)]
        public string Telephone { get; set; } 

        public string IdCivique { get; set; } 

        public IEnumerable<Entreprise> Entreprises { get; set; }

        public IEnumerable<Visite> Visites { get; set; }

    }
}
