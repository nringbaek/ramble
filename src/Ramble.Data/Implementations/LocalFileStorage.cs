using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ramble.Data.Implementations
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string FileBasePath;

        public LocalFileStorage(IOptionsMonitor<LocalFileStorageOptions> options)
        {
            FileBasePath = options.CurrentValue.BasePath;
        }

        public bool AddFile(string id, FileStream data)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public bool TryGetFile(string id, out FileStream data)
        {
            throw new NotImplementedException();
        }
    }
}
