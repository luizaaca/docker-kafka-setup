using Confluent.Kafka;
using System;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "192.168.99.100:9092",
                ClientId = "Consumer",
                AutoOffsetReset = AutoOffsetReset.Latest,
                GroupId = "TestConfluent",
                //AutoCommitIntervalMs = 5000
                //EnableAutoOffsetStore = false,
                //EnableAutoCommit = true,
                EnableAutoCommit = false
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("Topic");
                //count to check if consumer restarts from last read message in the broker
                //run again this consumer after 50 messages
                var count = 0;
                while (count < 50)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult.Message == null) break;

                    Console.WriteLine(consumeResult.Value);

                    //consumer.StoreOffset(consumeResult);
                    consumer.Commit(consumeResult);
                    count++;
                }

                consumer.Close();
            }

            Console.ReadLine();
        }
    }
}
