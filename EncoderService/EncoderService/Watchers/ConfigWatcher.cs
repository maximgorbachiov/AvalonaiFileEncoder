using CommonUtilities.Serializers;
using EncoderService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace EncoderService.Watchers
{
    public class ConfigWatcher
    {
        private readonly string configFilePath = $@"{Environment.CurrentDirectory}\map\map.json";
        private FileSystemWatcher watcher;

        private Dictionary<string, DirectoryWatcher> directoryWatchers = new Dictionary<string, DirectoryWatcher>();

        private bool enabled = true;

        ISerializer serializer = new JsonSerializer();

        public void Start()
        {
            if (!File.Exists(configFilePath))
            {
                File.Create(configFilePath);
            }

            this.watcher = new FileSystemWatcher(configFilePath);
            this.watcher.NotifyFilter = NotifyFilters.LastWrite;
            this.watcher.Filter = "*.json";
            this.watcher.Changed += OnChanged;
            this.watcher.EnableRaisingEvents = true;

            while (this.enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            this.watcher.EnableRaisingEvents = false;
            this.enabled = false;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            ConfigInfo configInfo;

            using (StreamReader reader = new StreamReader(configFilePath))
            {
                string content = reader.ReadToEnd();

                configInfo = this.serializer.Deserialize<ConfigInfo>(content);
            }

            if (configInfo != null)
            {
                foreach(string path in configInfo.Paths)
                {
                    if (!this.directoryWatchers.ContainsKey(path))
                    {
                        this.directoryWatchers.Add(path, new DirectoryWatcher(path));
                    }
                }

                var deletedDirectories = this.directoryWatchers.Select(dw => dw.Key).Except(configInfo.Paths);

                foreach(string deletedDirectory in this.directoryWatchers.Keys)
                {
                    this.directoryWatchers[deletedDirectory].Watcher.EnableRaisingEvents = false;
                    this.directoryWatchers.Remove(deletedDirectory);
                }
            }
        }
    }
}
