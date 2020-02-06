using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
    public class MonitoringTaskDto
    {
        public string id { get; set; }
        public string scheduleId { get; set; }
        public Priority priority { get; set; }
    }
}

namespace Common.Dto
{
    public enum Priority
    {
        Low, Medium, High
    }
}