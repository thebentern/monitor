using Monitor.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public sealed class ProcessMonitor : IMonitor<DefaultMessage>
    {
        private readonly ProcessStartInfo processStartInfo;
        private readonly IPublishMessages<DefaultMessage> messagePublisher;

        public ProcessMonitor( IPublishMessages<DefaultMessage> publisher, string process )
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            messagePublisher = publisher;

            processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = process
            };
        }
        public void Monitor()
        {
            StartProcess(MessageHandler);
        }
        public void MonitorAsync()
        {
            StartProcess(MessageHandlerAsync);
        }

        private void StartProcess(DataReceivedEventHandler messageHandler)
        {
            var process = new Process()
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            process.OutputDataReceived += messageHandler;
            process.ErrorDataReceived += messageHandler;

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }

        private void MessageHandler(object sender, DataReceivedEventArgs e)
        {
            var message = new DefaultMessage( messagePublisher.Channel, messagePublisher.Origin )
            {
                Content = e.Data
            };
            messagePublisher.Publish( message );
            //Should continue output from process (like tee)
            System.Console.WriteLine( e.Data );
        }
        private async void MessageHandlerAsync(object sender, DataReceivedEventArgs e)
        {
            var message = new DefaultMessage( messagePublisher.Channel, messagePublisher.Origin )
            {
                Content = e.Data
            };
            //Should continue output from process (like tee)
            System.Console.WriteLine( e.Data );
            await messagePublisher.PublishAsync( message );
        }
    }
}
