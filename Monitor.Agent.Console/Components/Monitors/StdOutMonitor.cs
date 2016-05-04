using System;
using System.IO;
using System.Text;

using Monitor.Core;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Monitor for StdIn stream of messages piped into the Console Agent
    /// </summary>
    public sealed class StdOutMonitor : IMonitor<DefaultMessage>
    {
        private readonly IPublishMessages<DefaultMessage> messagePublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="StdOutMonitor"/> class.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="stdInStream">The standard in stream.</param>
        /// <param name="stdOutStream">The standard out stream.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if any of the arguments are null
        /// </exception>
        public StdOutMonitor(IPublishMessages<DefaultMessage> publisher, Stream stdInStream, Stream stdOutStream)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            this.messagePublisher = publisher;

            if (stdInStream == null)
                throw new ArgumentNullException(nameof(stdInStream));
            StdInStream = stdInStream;

            if (stdOutStream == null)
                throw new ArgumentNullException(nameof(stdOutStream));
            StdOutStream = stdOutStream;
        }

        /// <summary>
        /// Gets or sets the standard out stream.
        /// </summary>
        /// <value>
        /// The standard out stream.
        /// </value>
        public Stream StdOutStream { get; set; }
        
        /// <summary>
        /// Gets or sets the standard in stream.
        /// </summary>
        /// <value>
        /// The standard in stream.
        /// </value>
        public Stream StdInStream { get; set; }

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
                    this.messagePublisher.Publish(CreateDefaultMessage(Encoding.GetEncoding(0).GetString(buffer)));
                    
                    // Should continue output from process (like tee)
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
                    await this.messagePublisher.PublishAsync(CreateDefaultMessage(Encoding.GetEncoding(0).GetString(buffer)));
                    
                    // Should continue output from process (like tee)
                    stdout.Write(buffer, 0, bytes);
                }
            }
        }

        /// <summary>
        /// Creates a DefaultMessage with the specified content
        /// </summary>
        /// <param name="messageContent">Content of the message.</param>
        /// <returns>DefaultMessage with the specified content</returns>
        private DefaultMessage CreateDefaultMessage(string messageContent)
        {
            return new DefaultMessage(this.messagePublisher.Channel, this.messagePublisher.Origin)
            {
                Content = messageContent
            };
        } 
   }
}
