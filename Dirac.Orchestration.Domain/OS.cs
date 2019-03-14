using System.Collections.Generic;

namespace Dirac.Orchestration.Domain
{
    public class OS : ValueObject
    {
        public string Name { get; set; }
        public string Version { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}