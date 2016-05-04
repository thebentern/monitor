using Monitor.Core;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// A generic interface for Monitoring a message originator
    /// </summary>
    /// <typeparam name="T">Implementors of IMessage</typeparam>
    public interface IMonitor<T> where T : IMessage
    {
        /// <summary>
        /// Monitors this instance of a message originator.
        /// </summary>
        void Monitor();

        /// <summary>
        /// Asynchronously monitors this instance of a message originator
        /// </summary>
        void MonitorAsync();
    }
}
