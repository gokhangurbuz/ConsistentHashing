using System;
using System.Collections.Generic;

namespace ConsistentHashing.Infrastructure
{
    public  class Constants
    {
        public static string RabbitHostName = "localhost";
        public static string RabbitUserName = "guest";
        public static string RabbitPassword = "guest";
        
        public static String CONSISTENT_HASH_EXCHANGE_TYPE = "x-consistent-hash";
        
        public static string EventPriorityExchange1 = "EventPriorityExchange1";
        public static string EventPriorityExchange2 = "EventPriorityExchange2";
        public static string EventPriorityExchange3 = "EventPriorityExchange3";
        public static string EventPriorityExchange4 = "EventPriorityExchange4";

        public static readonly List<string> Queues = new() {"Queue1", "Queue2", "Queue3", "Queue4", "Queue5"};
    }
}