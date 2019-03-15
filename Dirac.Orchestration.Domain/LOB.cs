using System.Collections.Generic;

namespace Dirac.Orchestration.Domain
{
    public class LOB : ValueObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}