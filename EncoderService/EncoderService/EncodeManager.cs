using EncoderService.Watchers;
using System.ServiceProcess;
using System.Threading;

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
            this.configWatcher.Stop();
        }
    }
}
