using System.Collections.Generic;
using System.Linq;

namespace gesteco.api.Configuration
{
    public class CorsOptions
    {
        public const string Cors = "Cors";

        public bool AllowAnyOrigin { get; set; } = false;
        public bool AllowAnyHeader { get; set; } = false;
        public bool AllowAnyMethod { get; set; } = false;
        public ICollection<string> AllowWithOrigins { get; set; } = new List<string>();

        public bool AllowAny => AllowAnyOrigin || AllowAnyHeader || AllowAnyMethod
                                || AllowWithOrigins.Any(o => !string.IsNullOrEmpty(o));
    }
}
