using System;
using System.Diagnostics;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Interface for Process hosting
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Gets or sets the message arguments creator.
        /// </summary>
        /// <value>
        /// The message arguments creator.
        /// </value>
        Func<DataReceivedEventArgs, MessageEventArgs> MessageArgsCreator { get; set; }
        /// <summary>
        /// Occurs when Message is received
        /// </summary>
        event EventHandler<MessageEventArgs> RaiseMessageReceived;
        /// <summary>
        /// Executes process with the specified message handler.
        /// </summary>
        /// <param name="handler">The message handler.</param>
        void Execute(EventHandler<MessageEventArgs> handler);
    }
}
