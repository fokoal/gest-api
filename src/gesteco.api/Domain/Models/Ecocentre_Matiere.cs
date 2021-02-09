using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Ecocentre_Matiere {

        [Key]
        public long Id { get; set; }

        public long IdEcocentre { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public bool Comptable { get; set; }


        [ForeignKey("IdEcocentre")]
        public Ecocentre Ecocentre { get; set; }
    }
}
