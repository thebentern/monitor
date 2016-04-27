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
        private readonly IPublishMessages<DefaultMessage> messagePublisher;
        public Stream StdOutStream;
        public Stream StdInStream;


        public StdOutMonitor(IPublishMessages<DefaultMessage> publisher, Stream stdInStream, Stream stdOutStream)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            messagePublisher = publisher;

            if (stdInStream == null)
                throw new ArgumentNullException(nameof(stdInStream));
            StdInStream = stdInStream;

            if (stdOutStream == null)
                throw new ArgumentNullException(nameof(stdOutStream));
            StdOutStream = stdOutStream;
        }

        public void Monitor()
        {
            CaptureStdOut(Publish);
        }

        public void MonitorAsync()
        {
            CaptureStdOut(PublishAsync);
        }

        private void CaptureStdOut(Action<string> publish)
        {
            using (Stream stdin = StdInStream)
            using (Stream stdout = StdOutStream)
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
