using Monitor.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public sealed class StdOutMonitor : IMonitor<DefaultMessage>
    {
        private IPublishMessages<DefaultMessage> messagePublisher;

        public StdOutMonitor() { }

        public void Monitor(IPublishMessages<DefaultMessage> publisher)
        {
            messagePublisher = publisher;
            CaptureStdOut(Publish);
        }

        public void MonitorAsync(IPublishMessages<DefaultMessage> publisher)
        {
            messagePublisher = publisher;
            CaptureStdOut(PublishAsync);
        }

        private void CaptureStdOut(Action<string> publish)
        {
            using (Stream stdin = System.Console.OpenStandardInput())
            using (Stream stdout = System.Console.OpenStandardOutput())
            {
                byte[] buffer = new byte[2048];
                int bytes;
                while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Publish(Encoding.Default.GetString(buffer));
                    //Should continue output from process (like tee)
                    stdout.Write(buffer, 0, bytes);
                }
            }
        }

        private void Publish(string message)
        {
            var defaultMessage = new DefaultMessage(messagePublisher.Channel, messagePublisher.Origin)
            {
                Content = message
            };
            messagePublisher.Publish(defaultMessage);
        }

        private async void PublishAsync(string message)
        {
            var defaultMessage = new DefaultMessage(messagePublisher.Channel, messagePublisher.Origin)
            {
                Content = message
            };
            await messagePublisher.PublishAsync(defaultMessage);
        } 
   }
}
