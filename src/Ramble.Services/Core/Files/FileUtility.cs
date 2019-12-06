using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ramble.Services.Core.Files
{
    public static class FileUtility
    {
        public static string GetFileContentType(string filename) => Path.GetExtension(filename) switch
        {
            "gif" => "image/gif",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "mp3" => "audio/mpeg",
            "mpeg" => "video/mpeg",
            "png" => "image/png",
            "svg" => "image/svg+xml",
            "wav" => "audio/wav",
            "weba" => "audio/webm",
            "webm" => "video/webm",
            "webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}
