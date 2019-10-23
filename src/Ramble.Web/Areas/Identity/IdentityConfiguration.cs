using System.Collections.Generic;

namespace Ramble.Web.Areas.Identity
{
    public class IdentityConfiguration
    {
        public string BaseUrl { get; set; }
        public string Secret { get; set; }
        public string DbConnectionString { get; set; }

        public List<string> ProtectedEndpoints { get; set; } = new List<string>();
    }
}
