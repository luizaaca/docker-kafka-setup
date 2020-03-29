using Confluent.Kafka;
using System;

namespace KafkaPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "192.168.99.100:9092",
                ClientId = "Producer",
                Acks = Acks.All
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                for (int i = 0; i < 100; i++)
                {
                    producer
                        .ProduceAsync("Topic", new Message<Null, string> { Value = $"Test Confluent {i}" })
                        .ContinueWith((task) =>
                         {
                             Console.WriteLine("Status: {0}, Message: {1}, Offset: {2}", task.Result.Status, task.Result.Value, task.Result.Offset.Value);
                         })
                        .Wait();
                }
            }

            Console.ReadLine();
        }
    }
}
