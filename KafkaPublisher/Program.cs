using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;

namespace KafkaPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://192.168.99.100:9092");
            var options = new KafkaOptions(uri);
            string topic = "TestTopic-1";
            var router = new BrokerRouter(options);
            var client = new Producer(router);

            for (int i = 0; i < 10; i++)
            {
                string payload = $"Welcome to Kafka {i+1}! - {topic}";
                Message msg = new Message(payload);
                client.SendMessageAsync(topic, new List<Message> { msg }).Wait();
            }

            topic = "TestTopic-2";
            router = new BrokerRouter(options);
            client = new Producer(router);

            for (int i = 0; i < 10; i++)
            {
                string payload = $"Welcome to Kafka {i + 1} - {topic}!";
                Message msg = new Message(payload);
                client.SendMessageAsync(topic, new List<Message> { msg }).Wait();
            }

            Console.ReadLine();
        }
    }
}
