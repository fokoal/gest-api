using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class VisiteDTO {

        
        public long IdVisite { get; set; }
      
        public long IClient { get; set; }
   
        public long IdProvenance { get; set; }
 
        public long IdEcocentre { get; set; }
        [Required]
        [MaxLength(250)]
        public string Employe { get; set; }
      
        public ProvenanceDTO Provenance { get; set; }
      
        public ClientDTO Client { get; set; }
      
        public TransactionDTO Transaction { get; set; }
       
        public EcocentreDTO Ecocentre { get; set; }

        public IEnumerable<Matiere_VisiteDTO> Matieres { get; set; }

        public DateTime DateCreation { get; set; }

        public string DateVisite
        {
            get
            {
                return this.DateCreation.Date.ToShortDateString();
            }

        }
        public string NumeroFacture
        {
            get
            {
                var tmp_numeroTransaction = IdVisite.ToString();
                tmp_numeroTransaction = tmp_numeroTransaction.Length < 8 ? new StringBuilder(tmp_numeroTransaction).Insert(0, "0", (int)8 - tmp_numeroTransaction.Length).ToString() : tmp_numeroTransaction;
                return tmp_numeroTransaction;
            }

        }
    }
}
