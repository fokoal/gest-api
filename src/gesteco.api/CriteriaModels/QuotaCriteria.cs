using System;

namespace gesteco.api.src.gesteco.WebApi.CriteriaModels {
    public class QuotaCriteria {

        public string Adresse { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string IdCivique { get; set; }
        public int DefaultNumber { get; set; } = 5000;

    }
}
