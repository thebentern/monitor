using System;
using System.Diagnostics;

namespace Monitor.Agent
{
    public sealed class ProcessMonitor : IMonitor
    {
        private ProcessStartInfo processStartInfo;
        public ProcessMonitor(string process, string args)
        {
            processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = args,
                FileName = process
            };
        }
        public void Monitor()
        {
            var process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }
    }
}