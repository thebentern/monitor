using System;
using System.Collections.Generic;

namespace Monitor.Core
{
    /// <summary>
    /// Default Message class
    /// </summary>
    public class DefaultMessage : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMessage"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="origin">The origin.</param>
        public DefaultMessage( string channel = "Default", 
                               string origin  = "Default" )
        {
            Channel = channel;
            Origin = origin;
        }
        /// <summary>
        /// Gets or sets the message subscription Channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public string Channel { get; set; }
        /// <summary>
        /// Gets or sets the message subscription Origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }
        /// <summary>
        /// Gets or sets the timestamp for the message.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the message Content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the Custom Attributes for the message.
        /// </summary>
        /// <value>
        /// The custom attributes.
        /// </value>
        public Dictionary<string, string> CustomAttributes { get; set; }
    }
}
