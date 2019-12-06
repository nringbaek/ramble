using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Web
{
    public static class RambleConstants
    {
        public static class AuthenticationSchemes
        {
            public const string Cookies = "RambleCookies";
            public const string Oidc = "RambleOidc";
            public const string RambleManagementApi = "RambleManagementApi";
        }
    }
}
