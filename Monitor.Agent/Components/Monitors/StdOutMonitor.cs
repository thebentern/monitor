using System;
using System.Diagnostics;
using System.IO;

namespace Monitor.Agent
{
    public sealed class StdOutMonitor : IMonitor
    {
        public StdOutMonitor(){}
        public void Monitor()
        {
            using (Stream stdin = Console.OpenStandardInput())
            using (Stream stdout = Console.OpenStandardOutput())
            {
                byte[] buffer = new byte[2048];
                int bytes;
                while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0) {
                    stdout.Write(buffer, 0, bytes);
                }
            }
        }
    }
}