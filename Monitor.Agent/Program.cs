using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Monitor.Agent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IMonitor process;
            
            if(args == null || args.Length == 0)
                process = new StdOutMonitor();
            else
                process = new ProcessMonitor(args[0], String.Join(" ", args.Skip(1)));
                
            process.Monitor();
        }
    }
}