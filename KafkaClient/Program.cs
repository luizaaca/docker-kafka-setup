using KafkaNet;
using KafkaNet.Model;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string topic = "TestTopic-1";
            Uri uri = new Uri("http://192.168.99.100:9092");
            var options = new KafkaOptions(uri);
            var router = new BrokerRouter(options);
            var consumer1 = new Consumer(new ConsumerOptions(topic, router));
            Task.Run(() =>
            {
                foreach (var message in consumer1.Consume())
                {
                    Console.WriteLine(Encoding.UTF8.GetString(message.Value));
                    //Console.WriteLine("Response: P{0},O{1} : {2}",
                    //    message.Meta.PartitionId,
                    //    message.Meta.Offset,
                    //    message.Value);
                }
            });
            topic = "TestTopic-2";
            var consumer2 = new Consumer(new ConsumerOptions(topic, router));
            Task.Run(() =>
            {
                foreach (var message in consumer2.Consume())
                    Console.WriteLine(Encoding.UTF8.GetString(message.Value));
            });
            Console.ReadLine();
        }
    }
}
