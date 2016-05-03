using Monitor.Core;
using System;
using System.IO;
using System.Text;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Monitor for StdIn stream of messages piped into the Console Agent
    /// </summary>
    public sealed class StdOutMonitor : IMonitor<DefaultMessage>
    {
        private readonly IPublishMessages<DefaultMessage> _messagePublisher;

        public Stream StdOutStream;
        public Stream StdInStream;


        public StdOutMonitor(IPublishMessages<DefaultMessage> publisher, Stream stdInStream, Stream stdOutStream)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            _messagePublisher = publisher;

            if (stdInStream == null)
                throw new ArgumentNullException(nameof(stdInStream));
            StdInStream = stdInStream;

            if (stdOutStream == null)
                throw new ArgumentNullException(nameof(stdOutStream));
            StdOutStream = stdOutStream;
        }

        /// <summary>
        /// Monitors this instance of the StdIn stream of messages coming into the Console Agent
        /// </summary>
        public void Monitor()
        {
            using (Stream stdin = StdInStream)
            using (Stream stdout = StdOutStream)
            {
                byte[] buffer = new byte[2048];
                int bytes;
                while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0)
                {
                    _messagePublisher.Publish(CreateDefaultMessage(Encoding.GetEncoding(0).GetString(buffer)));
                    //Should continue output from process (like tee)
                    stdout.Write(buffer, 0, bytes);
                }
            }
        }

        /// <summary>
        /// Asynchronously monitors the StdIn stream of messages coming into the Console Agent
        /// </summary>
        public async void MonitorAsync()
        {
            using (Stream stdin = StdInStream)
            using (Stream stdout = StdOutStream)
            {
                byte[] buffer = new byte[2048];
                int bytes;
                while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await _messagePublisher.PublishAsync(CreateDefaultMessage(Encoding.GetEncoding(0).GetString(buffer)));
                    //Should continue output from process (like tee)
                    stdout.Write(buffer, 0, bytes);
                }
            }
        }

        /// <summary>
        /// Creates a DefaultMessage with the specified content
        /// </summary>
        /// <param name="messageContent">Content of the message.</param>
        /// <returns></returns>
        private DefaultMessage CreateDefaultMessage(string messageContent)
        {
            return new DefaultMessage(_messagePublisher.Channel, _messagePublisher.Origin)
            {
                Content = messageContent
            };
        } 
   }
}
