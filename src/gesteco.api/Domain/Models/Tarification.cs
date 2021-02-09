using System.ComponentModel.DataAnnotations;

namespace gesteco.api.src.gesteco.WebApi.Database.Models {
    public class Tarification {

        [Key]
        public long IdTarification { get; set; } 

        public double Prix { get; set; } 

        public double Prix_Commerce { get; set; }


    }
}
