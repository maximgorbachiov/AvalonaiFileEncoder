using System.IO;
using System.Threading.Tasks;

namespace EncoderService.Encoder
{
    public interface IEncoder<TFrom, TTo> where TFrom : Stream where TTo : Stream
    {
        Task EncodeAsync(TFrom fromStream, TTo toStream, string password);
        Task DecodeAsync(TFrom fromStream, TTo toStream, string password);
    }
}
