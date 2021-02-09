using System;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Historique_Initialisation_Quota {

        [Key]
        public long Id { get; set; }

     
        public DateTime DateInit { get; set; }

        public  bool DateEncours { get; set; }

        [Required]
        public string Description { get; set; }
    }

}
