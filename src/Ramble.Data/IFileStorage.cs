using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ramble.Data
{
    public interface IFileStorage
    {
        bool TryGetFile(string id, out FileStream data);
        bool AddFile(string id, FileStream data);
        bool DeleteFile(string id);
    }
}
