using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class MatiereDTO {

  
        public long IdMatiere { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Selected { get; set; }
        public bool Comptable { get; set; }

     
    }
}
