using HotChocolate.Types;
using Ramble.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Data.GraphQl.Types
{
    public class WallType : ObjectType<WallEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<WallEntity> descriptor)
        {
            base.Configure(descriptor);
        }
    }
}
