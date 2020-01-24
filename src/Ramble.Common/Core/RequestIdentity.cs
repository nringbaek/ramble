using System.Collections.Generic;

namespace Ramble.Common.Core
{
    public class RequestIdentity
    {
        public bool IsAuthenticated { get; }

        public string UserId { get; }
        public List<string> Roles { get; }
        public Dictionary<string, object> Properties { get; }

        private RequestIdentity(bool isAuthenticated, string userId, List<string> roles, Dictionary<string, object> properties)
        {
            IsAuthenticated = isAuthenticated;
            UserId = userId;
            Roles = roles;
            Properties = properties;
        }

        public static RequestIdentity Authenticated(string userId, List<string> roles, Dictionary<string, object> properties = null!) =>
            new RequestIdentity(true, userId, roles, properties ?? new Dictionary<string, object>());

        public static RequestIdentity Anonymous(Dictionary<string, object> properties = null!) =>
            new RequestIdentity(false, null!, null!, properties ?? new Dictionary<string, object>());

        public object TryGetProperty<TType>(string key, out TType property)
        {
            if (Properties.TryGetValue(key, out var value))
            {
                property = (TType)value;
                return true;
            }

            property = default!;
            return false;
        }
    }
}
