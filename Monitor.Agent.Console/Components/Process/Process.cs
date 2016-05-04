using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Container class for executing a process using System.Diagnostics namespace
    /// </summary>
    public class Process : IProcess
    {
        private System.Diagnostics.Process process;

        private readonly ProcessStartInfo processStartInfo;
        /// <summary>
        /// Gets or sets the message arguments creator.
        /// </summary>
        /// <value>
        /// The message arguments creator.
        /// </value>
        public Func<DataReceivedEventArgs, MessageEventArgs> MessageArgsCreator { get; set; }

        /// <summary>
        /// Gets or sets the standard in text writer.
        /// </summary>
        /// <value>
        /// The standard in.
        /// </value>
        /// <remarks>
        /// This is currently used for testing purposes
        /// </remarks>
        public TextWriter StdIn => process.StandardInput;

        /// <summary>
        /// Initializes a new instance of the <see cref="Process" /> class.
        /// </summary>
        /// <param name="process">The process.</param>
        public Process(string process)
        {
            if (String.IsNullOrWhiteSpace(process))
                throw new ArgumentNullException(nameof(process));

            processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = process
            };
        }

        /// <summary>
        /// Raises the <see cref="E:RaiseMessageReceivedEvent" /> event.
        /// </summary>
        /// <param name="e">The <see cref="Monitor.Agent.Console.MessageEventArgs" /> instance containing the event data.</param>
        protected virtual void OnRaiseMessageReceivedEvent(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = RaiseMessageReceived;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when Message is received
        /// </summary>
        public event EventHandler<MessageEventArgs> RaiseMessageReceived;

        /// <summary>
        /// Executes the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void Execute(EventHandler<MessageEventArgs> handler)
        {
            process = new System.Diagnostics.Process()
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            RaiseMessageReceived += handler;
            process.OutputDataReceived +=
                (s, e) => OnRaiseMessageReceivedEvent(MessageArgsCreator.Invoke(e));
            process.ErrorDataReceived +=
                (s, e) => OnRaiseMessageReceivedEvent(MessageArgsCreator.Invoke(e));

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <remarks>
        /// This is currently used only for testing purposes
        /// </remarks>
        public void Execute(Action action)
        {
            processStartInfo.RedirectStandardInput = true;
            process = new System.Diagnostics.Process()
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            process.Start();
            action.Invoke();
            process.BeginOutputReadLine();
            //HACK Figure out a better way to do thiss
            Thread.Sleep(TimeSpan.FromSeconds(3));
            process.Close();
        }
    }
}
