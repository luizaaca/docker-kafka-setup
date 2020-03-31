using Confluent.Kafka;
using Newtonsoft.Json;
using System;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Consumer {args[0]}.{args[1]} iniciado.");
            var config = new ConsumerConfig
            {
                BootstrapServers = "192.168.99.100:9092",
                ClientId = args[0],
                AutoOffsetReset = AutoOffsetReset.Latest,
                GroupId = args[1],
                //AutoCommitIntervalMs = 5000
                //EnableAutoOffsetStore = false,
                //EnableAutoCommit = true,
                EnableAutoCommit = false
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(args[2]);
                //count to check if consumer restarts from last read message in the broker
                //run again this consumer after 50 messages
                var count = args[3] != null ? int.Parse(args[3]) : 10;

                while (count > 0)
                {
                    var consumeResult = consumer.Consume();

                    dynamic result = JsonConvert.DeserializeObject(consumeResult.Value);

                    Console.WriteLine($"Consumer {args[0]}.{args[1]} recebeu msg.");
                    Console.WriteLine($"Id = {result.payload.after.id} Prop = {result.payload.after.prop} em {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");

                    //consumer.StoreOffset(consumeResult);
                    consumer.Commit(consumeResult);

                    count--;
                }

                consumer.Close();
            }
            Console.WriteLine($"Consumer {args[0]}.{args[1]} encerrado.");
            Console.ReadLine();
        }
    }
}
