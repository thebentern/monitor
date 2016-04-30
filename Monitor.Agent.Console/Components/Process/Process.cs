using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Monitor.Core;

namespace Monitor.Agent.Console
{
    public class Process : IProcess
    {
        public Func<DataReceivedEventArgs, MessageEventArgs> MessageArgsCreator { get; set; }

        public TextWriter StdIn => process.StandardInput;

        private System.Diagnostics.Process process;

        private readonly ProcessStartInfo processStartInfo;
        public Process(string process)
        {
            if(String.IsNullOrWhiteSpace(process))
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
