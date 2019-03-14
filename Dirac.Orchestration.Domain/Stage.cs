using System.Collections.Generic;

namespace Dirac.Orchestration.Domain
{
    public class Stage:Entity
    {
        public List<Module> Modules { get; set; }
    }
}