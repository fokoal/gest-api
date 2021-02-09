using System;
using System.Text;

namespace gesteco.api.OutputModels {
    public class HistoriqueDTO {

        public string Adresse { get; set; }
        public DateTime DateVisite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Commerce { get; set; }
        public string Momtant { get; set; }
        public string Tel { get; set; }
        public string Ecocentre { get; set; }
        public string Avoir { get; set; }
        public string Employe { get; set; }
        public long Id { get; set; }

        public string Facture
        {
            get
            {
                var tmp_numeroTransaction = Id.ToString();
                tmp_numeroTransaction = tmp_numeroTransaction.Length < 8 ? new StringBuilder(tmp_numeroTransaction).Insert(0, "0", (int)8 - tmp_numeroTransaction.Length).ToString() : tmp_numeroTransaction;
                return tmp_numeroTransaction;
            }

        }

    }
}
