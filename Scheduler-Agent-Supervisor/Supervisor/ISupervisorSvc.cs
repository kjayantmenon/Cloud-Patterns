namespace Supervisor
{
    using Common.Dto;
    using System.Threading.Tasks;
    public interface ISupervisorSvc
    {
        Task CreateMonitor(AgentCommandDto commandDto);
        Task UpdateMonitor(AgentCommandDto commandDto);
    }
}