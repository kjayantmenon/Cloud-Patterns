
using System.Collections.Generic;

namespace Dirac.Orchestration.Domain
{
    public class Module:ValueObject, IExecutor
    {
        public static class ModuleFactory
        {
            public static Module CreateModule(string StageName)
            {
                var module = new Module();
                return module;
            }
        }
        public Algorithm algorithm { get; set; }
        public List<Module> Depends { get; private set; }

        protected Module()
        {
            Depends = new List<Module>();
        }

        public ExecutionResult Execute()
        {
            throw new System.NotImplementedException();
        }

        public ExecutionResult Kill()
        {
            throw new System.NotImplementedException();
        }

        public ExecutionResult Pause()
        {
            throw new System.NotImplementedException();
        }
    }
}