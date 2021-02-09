using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class AdresseDTO {
       
        public string IdCivique { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nom { get; set; }
    }
}
