using System;
using Monitor.Core;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Event arguments for Messages
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the Message.
        /// </summary>
        /// <value>
        /// The Message.
        /// </value>
        public IMessage Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public MessageEventArgs(IMessage message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

            Message = message;
        }
    }
}
