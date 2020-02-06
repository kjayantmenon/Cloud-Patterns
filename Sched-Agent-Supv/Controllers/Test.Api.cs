using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudPatterns
{
    [Route("api/v1/[controller]")]
    public class TestController:Controller
    {
        
        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            return Ok("Test");
        }
    }
}
