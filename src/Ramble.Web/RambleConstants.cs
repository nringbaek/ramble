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

        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Editor = "Editor";
            public const string Author = "Author";
        }
    }
}
