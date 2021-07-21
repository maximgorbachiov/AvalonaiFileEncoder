using EncoderService.Encoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderService.Watchers
{
    public class DirectoryWatcher
    {
        public FileSystemWatcher Watcher { get; }

        public FileStreamEncoder Encoder { get; }

        public DirectoryWatcher(string path)
        {
            this.Watcher = new FileSystemWatcher(path);
            this.Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            this.Watcher.Changed += OnChanged;
            this.Watcher.EnableRaisingEvents = true;
            this.Encoder = new FileStreamEncoder();
            this.RunEncoding();
        }

        private async Task RunEncoding()
        {
            await this.EncodingAsync(this.Watcher.Path);
        }

        private Task EncodingAsync(string folderPath)
        {
            return Task.Run(() =>
            {
                string[] filePathes = Directory.GetFiles(folderPath);

                this.Encoder.EncodeFiles(filePathes);
            });
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed || e.FullPath.EndsWith(".cphr"))
            {
                return;
            }

            
        }
    }
}
