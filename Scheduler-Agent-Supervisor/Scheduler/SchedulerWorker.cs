
namespace Scheduler
{
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Dto;
    using Common.Q;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;

    public class SchedulerWorker : QueueListener
    {
        private readonly ILogger<SchedulerWorker> _logger;
        private static string qName = "schedulesq";
        private static string hostName = "host.docker.internal";
        private readonly ISchedulerSvc _schedulerSvc;
        public SchedulerWorker(ILogger<SchedulerWorker> logger, ISchedulerSvc schedulerSvc) :base(hostName,qName)
        {
            _logger = logger;
            _schedulerSvc = schedulerSvc;



        }

        //private void InitQ()
        //{
        //    var factory = new ConnectionFactory { HostName = "host.docker.internal" };
        //    // create connection  
        //    _connection = factory.CreateConnection();
        //    // create channel  
        //    _channel = _connection.CreateModel();
        //    _channel.QueueDeclare(qName, false, false, false, null);
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //        await Task.Delay(1000, stoppingToken);
        //    }
        //}

        protected override async Task HandleMessageAsync(string content)
        {
            _logger.LogInformation($"consumer received {content}"); 
            var scheduleDto = JsonSerializer.Deserialize<ScheduleDto>(content);
            _schedulerSvc.CreateScheduleAsync(scheduleDto);
        }

      
    }
}
