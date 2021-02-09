using System;
using System.Collections.Generic;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class QuotaDTO {

        public long IdQuota { get; set; }
 
        public DateTime DateDebut { get; set; }
        
        public DateTime DateFin { get; set; }
    
        public long Quantite_Disponible { get; set; }
     
        public long Quantite_Commerce { get; set; }
   
        public long Quantite_Initiale { get; set; }
    
        public string IdCivique { get; set; }


        public AdresseDTO Adresse { get; set; }

        public IEnumerable<Historique_QuotaDTO> Historiques { get; set; }
    }
}
