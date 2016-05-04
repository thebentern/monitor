namespace Monitor.Agent.Console
{
    /// <summary>
    /// Structure for Console Agent arguments
    /// </summary>
    public class ConsoleAgentArgs
    {
        /// <summary>
        /// Gets or sets the Channel for the Message Subscription.
        /// </summary>
        /// <value>
        /// The channel name.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the Origin name for the Message Subscription.
        /// </summary>
        /// <value>
        /// The origin name.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the Host name.
        /// </summary>
        /// <value>
        /// The host name.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the Process string to execute in the Console Agent.
        /// </summary>
        /// <value>
        /// The process string.
        /// </value>
        public string Process { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Agent should capture standard out.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it should capture standard out; otherwise, <c>false</c>.
        /// </value>
        public bool StdOut { get; set; }
    }
}
