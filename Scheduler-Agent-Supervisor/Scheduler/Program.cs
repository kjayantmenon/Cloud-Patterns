using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Q;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddHostedService<SchedulerWorker>();
                    services.AddTransient<ISchedulerSvc, SchedulerSvc>();
                    services.AddTransient<IQClient, RabbitMQClient>();
                    //services.AddSingleton<SchedulerSvc>();
                });
    }
}
