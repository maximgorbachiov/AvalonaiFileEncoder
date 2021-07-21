using System;

namespace EncoderService.Converters
{
    public class ByteConverter
    {
        public byte[] ConvertIntToBytes(int length)
        {
            byte[] bytes = BitConverter.GetBytes(length);

            return bytes;
        }

        public byte[] ConvertWordToBytes(string word)
        {
            int bytesCount = word.Length * 2;
            byte[] bytes = new byte[bytesCount];

            int index = 0;

            foreach(char symbol in word)
            {
                byte[] symbolBytes = BitConverter.GetBytes(symbol);

                bytes[index++] = symbolBytes[0];
                bytes[index++] = symbolBytes[1];
            }

            return bytes;
        }
    }
}
