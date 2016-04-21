using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public class ConsoleAgentArgs
    {
        public string Channel { get; set; }
        public string Origin { get; set; }
        public string Host { get; set; }
        public string Process { get; set; }
        public bool StdOut { get; set; }
    }
}
