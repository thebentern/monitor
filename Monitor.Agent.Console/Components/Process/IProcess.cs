using System;
using System.Diagnostics;
using Monitor.Core;

namespace Monitor.Agent.Console
{
    public interface IProcess
    {
        Func<DataReceivedEventArgs, MessageEventArgs> MessageArgsCreator { get; set; }
        event EventHandler<MessageEventArgs> RaiseMessageReceived;
        void Execute(EventHandler<MessageEventArgs> handler);
    }
}
