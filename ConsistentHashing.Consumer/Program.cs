using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Constants = ConsistentHashing.Infrastructure.Constants;

namespace ConsistentHashing.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                ConnectionFactory cf = new ConnectionFactory();
                cf.HostName = Constants.RabbitHostName;
                cf.UserName = Constants.RabbitUserName;
                cf.Password = Constants.RabbitPassword;

                var conn = cf.CreateConnection();
                IModel channel = conn.CreateModel();
                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                    channel.BasicAck(ea.DeliveryTag,false);
                };

                foreach (var queue in Constants.Queues)
                {
                    var queueItem = channel.QueueDeclare(queue);
                    channel.BasicConsume(queue: queue, false, consumer);
                    channel.QueueBind(queue, Constants.EventPriorityExchange1, "1");
                    channel.QueueBind(queue, Constants.EventPriorityExchange2, "1");
                    channel.QueueBind(queue, Constants.EventPriorityExchange3, "1");
                    channel.QueueBind(queue, Constants.EventPriorityExchange4, "1");
                }

                Console.WriteLine("Exit with any key.");
                Console.ReadKey();
            }
        }
    }
}