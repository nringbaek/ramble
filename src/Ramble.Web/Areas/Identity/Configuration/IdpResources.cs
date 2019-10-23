using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Ramble.Web.Areas.Identity.Configuration
{
    public static class IdpResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("ramble.api", "Ramble API"),
                new ApiResource("ramble.admin.api", "Ramble Admin API")
            };
        }

        public static IEnumerable<Client> GetClients(string baseUrl, string secret)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ramble.admin.client",
                    ClientName = "Ramble Admin Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret(secret.Sha256())
                    },

                    RedirectUris           = { $"{baseUrl}/idp/_signin-oidc" },
                    PostLogoutRedirectUris = { $"{baseUrl}/idp/_signout-callback-oidc" },

                    AllowedScopes =
                    {
                        "ramble.api",
                        "ramble.admin.api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    AllowOfflineAccess = true
                }
            };
        }
    }
}
