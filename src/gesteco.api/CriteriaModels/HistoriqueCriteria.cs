using System;

namespace gesteco.api.src.gesteco.WebApi.CriteriaModels {
    public class HistoriqueCriteria {

        public string Adresse { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public DateTime? DateCreation { get; set; }
        public string Commerce { get; set; }
        public string Entreprise { get; set; }
        public string Tel { get; set; }
        public string Courriel { get; set; }
        public string Plaque { get; set; }
        public string Ecocentre { get; set; }
        public string ClientNom { get; set; }  
        public string Employe { get; set; }
        public int DefaultNumber { get; set; }
        public int PageNo
        {
            get;
            set;
        }
        public int RowsPerPage
        {
            get;
            set;
        }

    }
}
