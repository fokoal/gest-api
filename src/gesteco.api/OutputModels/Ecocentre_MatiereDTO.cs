using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class Ecocentre_MatiereDTO {

        public long Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public bool Comptable { get; set; }
        public bool Selected { get; set; }
        public EcocentreDTO Ecocentre { get; set; }
    }
}
