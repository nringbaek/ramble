using Microsoft.Extensions.Options;
using Ramble.Common;
using System.IO;
using System.Threading.Tasks;

namespace Ramble.Services.Core.Files
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _fileBasePath;

        public LocalFileStorage(IOptionsMonitor<LocalFileStorageOptions> options)
        {
            _fileBasePath = options.CurrentValue.BasePath;
        }

        public async Task<bool> AddFile(string id, byte[] data)
        {
            var (folder, filename) = SplitFileId(id, _fileBasePath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(Path.Combine(folder));

            await File.WriteAllBytesAsync(Path.Combine(folder, filename), data);

            return true;
        }

        public Task<bool> DeleteFile(string id)
        {
            var (folder, filename) = SplitFileId(id, _fileBasePath);
            var file = Path.Combine(folder, filename);
            if (File.Exists(file))
                File.Delete(file);

            return Task.FromResult(true);
        }

        public async Task<byte[]?> TryGetFile(string id)
        {
            var (folder, filename) = SplitFileId(id, _fileBasePath);
            var file = Path.Combine(folder, filename);
            if (File.Exists(file))
                return await File.ReadAllBytesAsync(file);

            return null;
        }

        private (string folder, string file) SplitFileId(string id, string folder) =>
            (Path.Combine(folder, id.Substring(0, 2)), id.Substring(2));
    }
}
