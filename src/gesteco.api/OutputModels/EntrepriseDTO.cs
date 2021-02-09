using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class EntrepriseDTO {

     
        public long IdEntreprise { get; set; }

     
        public long IdClient { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nom { get; set; }
      
      
    }
}
