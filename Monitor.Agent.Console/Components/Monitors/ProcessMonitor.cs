using System;
using System.Diagnostics;

using Monitor.Core;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Monitor for a process to be hosted in the Console Agent
    /// </summary>
    public class ProcessMonitor : IMonitor<DefaultMessage>
    {
        private readonly IProcess process;
        private readonly IPublishMessages<DefaultMessage> messagePublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMonitor"/> class.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="processInfo">The process information.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <param name="publisher"></param> or <param name="processInfo"></param> are null
        /// </exception>
        public ProcessMonitor(IPublishMessages<DefaultMessage> publisher, IProcess processInfo)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            
            if (processInfo == null)
                throw new ArgumentNullException(nameof(processInfo));

            messagePublisher = publisher;
            process = processInfo;
            process.MessageArgsCreator = CreateMessageArgs;
        }

        /// <summary>
        /// Monitors this instance of the process for messages
        /// </summary>
        public void Monitor()
        {
            StartProcess(MessageHandler);
        }

        /// <summary>
        /// Asynchronously monitors this instance of the process for messages
        /// </summary>
        public void MonitorAsync()
        {
            StartProcess(MessageHandlerAsync);
        }

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="messageHandler">The message handler.</param>
        private void StartProcess(EventHandler<MessageEventArgs> messageHandler)
        {
            this.process.Execute(messageHandler);
        }

        /// <summary>
        /// Creates the message arguments from DataReceivedEventArgs.
        /// </summary>
        /// <param name="e">The <see cref="DataReceivedEventArgs"/> instance containing the event data.</param>
        /// <returns>Message event arguments</returns>
        private MessageEventArgs CreateMessageArgs(DataReceivedEventArgs e)
        {
            var message = new DefaultMessage(this.messagePublisher.Channel, this.messagePublisher.Origin)
            {
                Content = e.Data
            };
            return new MessageEventArgs(message);
        }

        /// <summary>
        /// Handles the Message raised event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        private void MessageHandler(object sender, MessageEventArgs e)
        {
            this.messagePublisher.Publish((DefaultMessage)e.Message);

            // Should continue output from process (like tee)
            System.Console.WriteLine(e.Message.Content);
        }

        /// <summary>
        /// Handles the Message raised event asynchronously
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        private async void MessageHandlerAsync(object sender, MessageEventArgs e)
        {
            // Should continue output from process (like tee)
            System.Console.WriteLine(e.Message.Content);
            await this.messagePublisher.PublishAsync((DefaultMessage)e.Message);
        }
    }
}
