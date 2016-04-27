using Monitor.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public class ProcessMonitor : IMonitor<DefaultMessage>
    {
        private readonly IProcess process;
        private readonly IPublishMessages<DefaultMessage> messagePublisher;

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
        public void Monitor()
        {
            StartProcess(MessageHandler);
        }
        public void MonitorAsync()
        {
            StartProcess(MessageHandlerAsync);
        }

        private void StartProcess(EventHandler<MessageEventArgs> messageHandler)
        {
            process.Execute(messageHandler);
        }
        private MessageEventArgs CreateMessageArgs(DataReceivedEventArgs e)
        {
            var message = new DefaultMessage(messagePublisher.Channel, messagePublisher.Origin)
            {
                Content = e.Data
            };
            return new MessageEventArgs(message);
        }

        private void MessageHandler(object sender, MessageEventArgs e)
        {
            messagePublisher.Publish((DefaultMessage)e.Message);
            //Should continue output from process (like tee)
            System.Console.WriteLine(e.Message.Content);
        }
        private async void MessageHandlerAsync(object sender, MessageEventArgs e)
        {
            //Should continue output from process (like tee)
            System.Console.WriteLine(e.Message.Content);
            await messagePublisher.PublishAsync((DefaultMessage)e.Message);
        }
    }
}
