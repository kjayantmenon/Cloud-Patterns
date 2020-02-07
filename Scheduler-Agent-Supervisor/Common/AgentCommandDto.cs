using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
    public class AgentCommandDto
    {
        public string id { get; set; }
        public string command { get; set; }
        public IEnumerable<KeyValuePair<string,string>> parameters { get; set; }
        public string schedule { get; set; }
    }
}
