using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Strating Scheduler!");
            int i = 0;
            do
            {
                var schedule = CreateSchedule();
                SendCommand(JsonConvert.SerializeObject(schedule));
                Thread.Sleep(1000);
                i++;
            } while (i<10);
            Console.ReadKey();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static Schedule CreateSchedule()
        {
            var id =Guid.NewGuid().ToString();
            var schedule = new Schedule()
            {
                Id = id,
                Name = "new schedule " + RandomString(10)
            };
            return schedule;
        }

        private static void RunSchedule(string schedule)
        {
            for (int i = 1; i <= 10; i++)
            {
                SendCommand($"{schedule}");
            }
        }

        private async static Task SendCommand(string command)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    //string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(command);
                   
                    //message = $"Hello World! - {v}";

                    //body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                            routingKey: "hello",
                                            basicProperties: null,
                                            body: body);
                    Console.WriteLine(" [x] Sent {0}", command);
                    Task.Delay(1000).Wait();
                   
                }
            }
        }
    }



    public class Schedule
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
