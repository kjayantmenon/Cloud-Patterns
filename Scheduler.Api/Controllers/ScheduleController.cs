using Common;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Scheduler.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ScheduleController:Controller
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
        {
            return Ok();
        } 

        [HttpPost("update")]
        public IActionResult UpdateSchedule()
        {
            return Ok();
        }
    }
}
