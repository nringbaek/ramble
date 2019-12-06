using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramble.Web.Controllers.Management.Walls.Models
{
    public class UpdateWallEntryModel
    {
        public string EntryContent { get; set; }
        public DateTime? EntryTimestamp { get; set; }
    }
}
