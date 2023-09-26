using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sozluk.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName, string exchangedType, string queueName, object obj)
        {
            var channel = CreateBasicConsumer()
                .EnsureExchange(exchangeName, exchangedType)
                .EnsureQueue(queueName, exchangeName)
                .Model;

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(exchangeName, queueName, null, body);
        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory() { HostName = SozlukContants.RabbitMQHost };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, string exchangeName, string exchangeType = SozlukContants.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchangeName, exchangeType, false, false);
            return consumer;
        }
        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName, string exchangeName)
        {
            consumer.Model.QueueDeclare(queueName, false, false, false, null);
            consumer.Model.QueueBind(queueName, exchangeName, queueName);
            return consumer;
        }
    }
}
