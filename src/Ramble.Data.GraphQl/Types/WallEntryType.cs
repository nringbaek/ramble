using HotChocolate.Types;
using Ramble.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Data.GraphQl.Types
{
    public class WallEntryType : ObjectType<WallEntryEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<WallEntryEntity> descriptor)
        {
            base.Configure(descriptor);
        }
    }
}
