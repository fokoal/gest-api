using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Transaction {

        [Key]
        public long IdTransaction { get; set; }

        public long IdModePaiement { get; set; }

        public long IdVisite { get; set; }

        public long Hauteur { get; set; } 

        public long Largeur { get; set; } 

        public long Longueur { get; set; } 

        public long Quantite_Utilisee { get; set; }

        public double Total { get; set; }

        public double  Volume { get; set; } 

        [ForeignKey("IdModePaiement")]
        public ModePaiement ModePaiement { get; set; }

        [ForeignKey("IdVisite")]
        public Visite Visite { get; set; }


    }
}
