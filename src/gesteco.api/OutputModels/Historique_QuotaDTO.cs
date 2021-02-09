using System;

namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class Historique_QuotaDTO {

     
        public long IdHistorique_Quota { get; set; }  

        public DateTime DateHistorique { get; set; } 
      
        public DateTime DateDebut { get; set; }
 
        public DateTime DateFin { get; set; }
       
        public long Quantite_Utilisee { get; set; }  
        
        public long Quantite_Initiale { get; set; } 
        public string IdCivique { get; set; }

        public long IdQuota { get; set; } 

     
    }
}
