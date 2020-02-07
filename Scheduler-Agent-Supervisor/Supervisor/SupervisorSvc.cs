

namespace Supervisor
{
    using Common.Dto;
    using System;
    using System.Threading.Tasks;

    public class SupervisorSvc : ISupervisorSvc
    {
        public async Task CreateMonitor(AgentCommandDto commandDto)
        {
            
        }

        public Task UpdateMonitor(AgentCommandDto commandDto)
        {
            throw new NotImplementedException();
        }
    }
}
