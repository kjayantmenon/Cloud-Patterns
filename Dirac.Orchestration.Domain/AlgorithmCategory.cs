using System.Collections.Generic;

namespace Dirac.Orchestration.Domain
{
    public class AlgorithmCategory:ValueObject
    {
        public int Name { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}