using KafkaNet;
using KafkaNet.Model;
using System;
using System.Text;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string topic = "TestTopic";
            Uri uri = new Uri("http://192.168.99.100:9092");
            var options = new KafkaOptions(uri);
            var router = new BrokerRouter(options);
            var consumer = new Consumer(new ConsumerOptions(topic, router));
            foreach (var message in consumer.Consume())
                Console.WriteLine(Encoding.UTF8.GetString(message.Value));
            Console.ReadLine();
        }
    }
}
