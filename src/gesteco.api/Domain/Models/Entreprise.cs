using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Entreprise {

        [Key]
        public long IdEntreprise { get; set; }

        [Required]
        public long IdClient { get; set; }

       
        [MaxLength(100)]
        public string Nom { get; set; }

        [ForeignKey("IClient")]
        public Client Client { get; set; }

    }
}
