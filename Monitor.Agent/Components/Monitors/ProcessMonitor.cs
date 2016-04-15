using System;
using System.Diagnostics;
using System.IO;

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
            process.OutputDataReceived += OutputReceived;
            process.ErrorDataReceived += OutputReceived;
            

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }
        public void OutputReceived(object sender, DataReceivedEventArgs e) 
        {
            Console.WriteLine(e.Data);
        }
    }
}