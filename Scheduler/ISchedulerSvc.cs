namespace Scheduler
{
    using Common.Dto;
    using System.Threading.Tasks;

    public interface ISchedulerSvc
    {
        Task CreateScheduleAsync(ScheduleDto scheduleDto);
        Task UpdateSchedulerAsync(ScheduleDto scheduleDto);
    }
}