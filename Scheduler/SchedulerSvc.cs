


namespace Scheduler
{
    using Common;
    using Common.Dto;
    using Common.Q;
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class SchedulerSvc: ISchedulerSvc
    {

        public SchedulerSvc()
        {

        }
        public async Task CreateScheduleAsync(ScheduleDto scheduleDto )
        {
            dispatchCommand(scheduleDto.id, scheduleDto.name);
            broadcastSchedule(scheduleDto.id, scheduleDto.name);
        }

        private async Task broadcastSchedule(string scheduleId, string commandName)
        {

            var agentQClient = getMonitoringQClient();
            var monitoringTaskDto = new MonitoringTaskDto();
            monitoringTaskDto.id = Guid.NewGuid().ToString();
            monitoringTaskDto.scheduleId = scheduleId;
            monitoringTaskDto.priority = Priority.High;
            var monitoringMessage = JsonSerializer.Serialize(monitoringTaskDto);
            agentQClient.PublishAsync(monitoringMessage);

        }

        private async Task dispatchCommand(string scheduleId, string commandName)
        {
            
            List<KeyValuePair<string,string>> cmdParams = new List<KeyValuePair<string, string>>();
            var param1 = new KeyValuePair<string, string>("param-1", "test");
            cmdParams.Add(param1);
            var dummyCommand = new AgentCommandDto();
            dummyCommand.command = commandName;
            dummyCommand.schedule = scheduleId;
            dummyCommand.parameters = cmdParams;

            dummyCommand.id = Guid.NewGuid().ToString();
            var scheduleMessage = JsonSerializer.Serialize(dummyCommand);

            var agentQClient = getAgentQClient();
            agentQClient.PublishAsync(scheduleMessage);
        }

        private IQClient getAgentQClient()
        {
            var hostName = "host.docker.internal";
            var qName = "agentJobIPQ";
            var port = "";
            return getQClient(hostName, qName);
        }

        private IQClient getMonitoringQClient()
        {
            var hostName = "host.docker.internal";
            var qName = "monitoringQ";
            var port = "";
            return getQClient(hostName, qName);
        }

        private IQClient getQClient(string hostName, string qName)
        {
            return new RabbitMQClient(hostName,qName);
            
        }

        public async Task UpdateSchedulerAsync(ScheduleDto scheduleDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
