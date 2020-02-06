using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Supervisor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Supervisor starts");
            Init();
            Supervise();
        }

        private async static void Listen()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += MessageHandler;
                //consumer.Received += (model, ea) =>
                //{
                //    var body = ea.Body;
                //    var message = Encoding.UTF8.GetString(body);
                //    Console.WriteLine(" [x] Received {0}", message);
                //};
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        static IMongoDatabase database = null;
        static string _scheduleCollectionName = "schedules";
        public static void MessageHandler(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            var schedule = JsonConvert.DeserializeObject<Schedule>(message);
            //var status =jobMap.TryGetValue(message)??;
           //Get the schedule if it already exists

           //Else, create a new schedule
           //Update the schedule and persist
        }


        private static void Supervise()
        {
            //Start Listener
            Listen();
            //Get list of schedules
            

        }

        private static IMongoCollection<Schedule> _schedules;
        private static void Init()
        {
            var databaseName = "schedulesdb";
            var settings = new ScheduleDatabaseSettings();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            
            database = client.GetDatabase(databaseName);
            _schedules = database.GetCollection<Schedule>(_scheduleCollectionName);
            
        }

        private static void ConnectToMongoDb(ScheduleDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _schedules = database.GetCollection<Schedule>(settings.schedulesCollectionName);
        }

    }

    public class Schedule
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Status")]
        public ScheduleStatus Status { get; set; }


    }
    public class ScheduleDatabaseSettings
    {
        public string schedulesCollectionName
        {
            get
            {
                return "Schedules";
            }
        }

        public string ConnectionString
        {
            get
            {
                return "mongodb://localhost:27017";
            }
        }

        public string DatabaseName
        {
            get
            {
                return "SchedulesDb";
            }
        }

    }

    public class CustomTask
    {

        public CustomTask()
        {

        }

        
    }

    public enum ScheduleStatus
    {
        Unknown, InProgress, Completed, Faulted,Ready
    }
}
 

