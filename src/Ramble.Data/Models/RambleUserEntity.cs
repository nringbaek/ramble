﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble.Data.Models
{
    public class RambleUserEntity : IdentityUser
    {
        public List<WallEntity> CreatedWalls { get; set; } = null!;
        public List<WallEntryEntity> CreatedWallEntries { get; set; } = null!;
    }
}
