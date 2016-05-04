namespace Monitor.Agent.Console
{
    /// <summary>
    /// Strongly typed application config
    /// </summary>
    /// <remarks>
    /// See Config.yml for actual configuration
    /// </remarks>
    public sealed class AppConfig
    {
        /// <summary>
        /// Gets or sets the host connection string.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }
    }
}
