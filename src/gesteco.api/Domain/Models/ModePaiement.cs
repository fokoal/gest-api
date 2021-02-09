using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class ModePaiement {


        [Key]
        public long IdModePaiement { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
