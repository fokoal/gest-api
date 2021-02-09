using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class ModePaiementDTO {

         
        public long IdModePaiement { get; set; }

        [Required]
        [MaxLength(300)]
        public string Nom { get; set; } 

        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
