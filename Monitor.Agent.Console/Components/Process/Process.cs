using System;
using System.Diagnostics;
using Monitor.Core;

namespace Monitor.Agent.Console
{
    public class Process : IProcess
    {
        public Func<DataReceivedEventArgs, MessageEventArgs> MessageArgsCreator { get; set; }
        private readonly ProcessStartInfo processStartInfo;
        public Process(string process)
        {
            if(process == null)
                throw new ArgumentNullException(nameof(process));

            processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = process
            };
        }
        protected virtual void OnRaiseMessageReceivedEvent(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = RaiseMessageReceived;
            handler?.Invoke(this, e);
        }
        public event EventHandler<MessageEventArgs> RaiseMessageReceived;
        public void Execute(EventHandler<MessageEventArgs> handler)
        {
            var process = new System.Diagnostics.Process()
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            RaiseMessageReceived += handler;
            process.OutputDataReceived += (s, e) => RaiseMessageReceived(s, MessageArgsCreator.Invoke(e));
            process.ErrorDataReceived += (s, e) => RaiseMessageReceived(s, MessageArgsCreator.Invoke(e));

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }
    }
}
