using System;
using System.Text;
using RabbitMQ.Client;
using Constants = ConsistentHashing.Infrastructure.Constants;

namespace ConsistentHashing.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory cf = new ConnectionFactory();
            cf.HostName = Constants.RabbitHostName;
            cf.UserName = Constants.RabbitUserName;
            cf.Password = Constants.RabbitPassword;

            var conn = cf.CreateConnection();
            IModel ch = conn.CreateModel();

            ch.ExchangeDeclare(Constants.EventPriorityExchange1, Constants.CONSISTENT_HASH_EXCHANGE_TYPE, true, false, null);
            ch.ExchangeDeclare(Constants.EventPriorityExchange2, Constants.CONSISTENT_HASH_EXCHANGE_TYPE, true, false, null);
            ch.ExchangeDeclare(Constants.EventPriorityExchange3, Constants.CONSISTENT_HASH_EXCHANGE_TYPE, true, false, null);
            ch.ExchangeDeclare(Constants.EventPriorityExchange4, Constants.CONSISTENT_HASH_EXCHANGE_TYPE, true, false, null);

            ch.ConfirmSelect();

            for (int i = 0; i < 1000000; i++)
            {
                var body = Encoding.UTF8.GetBytes(i.ToString());

                ch.BasicPublish(Constants.EventPriorityExchange1, i.ToString(), ch.CreateBasicProperties(), body);
                ch.BasicPublish(Constants.EventPriorityExchange2, i.ToString(), ch.CreateBasicProperties(), body);
                ch.BasicPublish(Constants.EventPriorityExchange3, i.ToString(), ch.CreateBasicProperties(), body);
                ch.BasicPublish(Constants.EventPriorityExchange4, i.ToString(), ch.CreateBasicProperties(), body);
            }

            Console.WriteLine("Publish completed.");
        }
    }
}