
namespace Supervisor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Q;
    using Microsoft.Extensions.Logging;

    public class SupervisorWorker : QueueListener
    {
        private readonly ILogger<SupervisorWorker> _logger;
        private static string hostName = "host.docker.internal";
        private static string qName = "monitoringQ";
        public SupervisorWorker(ILogger<SupervisorWorker> logger):base(hostName,qName )
        {
            _logger = logger;
            
            //InitQ(hostName, qName);
        }

        protected override async Task HandleMessageAsync(string content)
        {
            _logger.LogInformation($"monitoring command {content}");
        }
    }
}
