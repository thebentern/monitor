using System;
using System.Collections.Generic;

namespace Monitor.Core
{
    /// <summary>
    /// Interface for Messages
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets or sets the message subscription Channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        string Channel { get; set; }
        /// <summary>
        /// Gets or sets the message subscription Origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        string Origin { get; set; }
        /// <summary>
        /// Gets or sets the message Timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the message Content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        string Content { get; set; }
        /// <summary>
        /// Gets or sets the message's Custom Attributes.
        /// </summary>
        /// <value>
        /// The custom attributes.
        /// </value>
        Dictionary<string, string> CustomAttributes { get; set; }
    }
}