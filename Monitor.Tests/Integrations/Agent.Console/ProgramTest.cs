using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Monitor.Agent.Console;
using Monitor.Core;
using Monitor.Handlers.Redis;
using Monitor.Tests.Integrations.Redis;
using NUnit.Framework;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ProgramTest : RedisBaseTest
    {   
        [Test]
        public void Publishes_Message_From_Process()
        {
            redisSubscriber.Subscribe(CheckMessage);
            Program.Main(new []{ "-p", "ipconfig" });
        }

        [Test]
        public void Publishes_Message_From_StdIn()
        {
            //TODO
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().NotBeNull();
        }
    }
}
