using EncoderService.Watchers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EncoderService
{
    public partial class EncodeManager : ServiceBase
    {
        private ConfigWatcher configWatcher;

        public EncodeManager()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            this.configWatcher = new ConfigWatcher();
            ThreadPool.QueueUserWorkItem((data) => { this.configWatcher.Start(); });
        }

        protected override void OnStop()
        {
        }
    }
}
