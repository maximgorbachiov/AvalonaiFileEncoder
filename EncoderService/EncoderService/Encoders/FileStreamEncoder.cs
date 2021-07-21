using EncoderService.Configuration;
using EncoderService.Converters;
using System.IO;
using System.Threading.Tasks;

namespace EncoderService.Encoders
{
    public class FileStreamEncoder : IFileStreamEncoder
    {
        public ByteConverter ByteConverter { get; }
        public IEncoder<FileStream, FileStream> Encoder { get; }

        public FileStreamEncoder()
        {
            this.ByteConverter = new ByteConverter();
            this.Encoder = new XorEncoder<FileStream, FileStream>();
        }

        public async Task EncodeFilesAsync(string[] filePathes)
        {
            foreach (string filePath in filePathes)
            {
                await this.EncodeFileAsync(filePath);
            }
        }

        public async Task EncodeFileAsync(string filePath)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);

            using (FileStream encodedFile = new FileStream($"{fileNameWithoutExt}.cphr", FileMode.OpenOrCreate))
            {
                byte[] extInBytes = this.ByteConverter.ConvertWordToBytes(fileExtension);

                int extLength = extInBytes.Length;
                byte[] lengthInBytes = this.ByteConverter.ConvertIntToBytes(extLength);

                encodedFile.Write(lengthInBytes, 0, lengthInBytes.Length);
                encodedFile.Write(extInBytes, 0, extLength);

                using (FileStream fileToEncode = new FileStream(filePath, FileMode.Open))
                {
                    await this.Encoder.EncodeAsync(fileToEncode, encodedFile, ConfigurationReader.Configuration.Password);
                }
            }
        }

        public Task DecodeFileAsync(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
