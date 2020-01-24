using System.Threading.Tasks;

namespace Ramble.Common
{
    public interface IFileStorage
    {
        Task<byte[]?> TryGetFile(string id);
        Task<bool> AddFile(string id, byte[] data);
        Task<bool> DeleteFile(string id);
    }
}
