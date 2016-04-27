using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Monitor.Agent.Console;
using Monitor.Core;
using Monitor.Tests.Integrations.Redis;
using NUnit.Framework;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ProcessMonitorTest : RedisBaseTest
    {
        private MemoryStream streamIn;
        private MemoryStream streamOut;

        private StreamWriter writer;

        [SetUp]
        public void Init_Stream()
        {
            streamIn = new MemoryStream();
            streamOut = new MemoryStream();
            writer = new StreamWriter(streamIn);
        }
        [TearDown]
        public void Close_Stream()
        {
            streamIn?.Close();
            streamOut?.Close();
        }
        
        private void CheckMessage(IMessage message)
        {
            message.Content.Should().Be("Derp");
        }
    }
}
