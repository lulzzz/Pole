﻿using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Pole.EventBus.RabbitMQ
{
    public class RabbitMQClient : IRabbitMQClient
    {
        readonly ConnectionFactory connectionFactory;
        readonly RabbitOptions options;
        readonly DefaultObjectPool<ModelWrapper> pool;
        public RabbitMQClient(IOptions<RabbitOptions> config)
        {
            options = config.Value;
            connectionFactory = new ConnectionFactory
            {
                Port=options.Port,
                UserName = options.UserName,
                Password = options.Password,
                VirtualHost = options.VirtualHost,
                AutomaticRecoveryEnabled = true
            };
            pool = new DefaultObjectPool<ModelWrapper>(new ModelPooledObjectPolicy(connectionFactory, options));
        }

        public ModelWrapper PullChannel()
        {
            var result = pool.Get();
            if (result.Pool is null)
                result.Pool = pool;
            return result;
        }
    }
}
