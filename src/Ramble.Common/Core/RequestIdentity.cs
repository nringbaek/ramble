using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Common.Core
{
    public class RequestIdentity
    {
        public bool IsAuthenticated { get; set; } = false;

        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
