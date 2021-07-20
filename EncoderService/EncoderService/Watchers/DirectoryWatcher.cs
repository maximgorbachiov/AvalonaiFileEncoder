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

        public DirectoryWatcher(string path)
        {
            this.Watcher = new FileSystemWatcher(path);
            this.Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            this.Watcher.Changed += OnChanged;
            this.Watcher.EnableRaisingEvents = true;
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
