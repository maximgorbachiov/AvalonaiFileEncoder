using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderService.Encoders
{
    public interface IFileStreamEncoder
    {
        Task EncodeFileAsync(string filePath);
        Task EncodeFilesAsync(string[] filePathes);
        Task DecodeFileAsync(string filePath);
    }
}
