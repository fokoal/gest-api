using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gesteco.api.Configuration {
    public class AzureAdOptions {

        public const string AzureAd = "AzureAd";

        public string Instance { get; set; }  
        public string Domain { get; set; }  
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string CallbackPath { get; set; }
        public string ClientSecret { get; set; }
        public string UserId { get; set; }
        public string ResourceId { get; set; }
        public ICollection<string> Scopes { get; set; } = new List<string>();
    }
}
