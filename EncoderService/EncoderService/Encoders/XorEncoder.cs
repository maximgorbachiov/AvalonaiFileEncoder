using System.IO;
using System.Threading.Tasks;

namespace EncoderService.Encoders
{
    public class XorEncoder<TFrom, TTo> : IEncoder<TFrom, TTo> where TFrom : Stream where TTo : Stream
    {
        private const int MEGA_BYTE = 1024 * 1024;

        public async Task DecodeAsync(TFrom fromStream, TTo toStream, string password)
        {
            await this.EncodeAsync(fromStream, toStream, password);
        }

        public async Task EncodeAsync(TFrom fromStream, TTo toStream, string password)
        {
            int count, lastPosition = 0;
            byte[] readBuffer = new byte[MEGA_BYTE], writeBuffer;
            byte[] passwordBytes = this.GetPasswordBytes(password);

            await Task.Run(() =>
            {
                while ((count = fromStream.Read(readBuffer, 0, MEGA_BYTE)) > 0)
                {
                    writeBuffer = new byte[count];

                    for (int i = 0; i < count; i++)
                    {
                        writeBuffer[i] = (byte)(readBuffer[i] ^ passwordBytes[lastPosition]);

                        lastPosition++;
                        if (lastPosition == passwordBytes.Length)
                        {
                            lastPosition = 0;
                        }
                    }

                    toStream.Write(writeBuffer, 0, count);
                }
            });
        }

        private byte[] GetPasswordBytes(string password)
        {
            int k = 0;
            ushort highByte = 65280, lowByte = 255;
            byte[] passwordBytes = new byte[password.Length * 2];
            char[] passwordChars = password.ToCharArray();

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordBytes[i] = (byte)(passwordChars[k] & highByte);
                passwordBytes[++i] = (byte)(passwordChars[k] & lowByte);
                k++;
            }

            return passwordBytes;
        }
    }
}
