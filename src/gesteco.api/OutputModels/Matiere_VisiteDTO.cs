using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class Matiere_VisiteDTO {

        public long IdMatiere_Visite { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }  

        public bool Comptable { get; set; }  
        public VisiteDTO Visite { get; set; }


    }
}
