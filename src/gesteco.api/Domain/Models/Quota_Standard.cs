using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Quota_Standard {

        [Key]
        public long Id { get; set; }
        [Required]
        public long Quantite { get; set; }

        public long Quantite_Commerce { get; set; }
    }
}
