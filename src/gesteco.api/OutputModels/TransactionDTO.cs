namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class TransactionDTO {

      
        public long IdTransaction { get; set; }

        public long IdModePaiement { get; set; }

        public long IdVisite { get; set; }

        public long Hauteur { get; set; }  

        public long Largeur { get; set; } 

        public long Longueur { get; set; } 

        public long Quantite_Utilisee { get; set; }  

        public double Total { get; set; }  

        public double Volume { get; set; }  
      
        public ModePaiementDTO ModePaiement { get; set; }

        public string Mesures
        {
            get
            {
                return   string.Format( "Long = {0}  Larg= {1}  Haut= {2}" ,  this.Longueur , this.Largeur, this.Hauteur);
            }

        }


    }
}
