using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    //public class RedisMessagePublisher : IPublishMessages, IDisposable
    //{
    //    ConnectionMultiplexer redis;

    //    public RedisMessagePublisher(string host, string channel)
    //    {
    //        redis = ConnectionMultiplexer.Connect(host);
    //    }

    //    public void Dispose()
    //    {
    //        redis?.Close();
    //    }

    //    public void Publish(Message message)
    //    {

    //    }
    //}
}
