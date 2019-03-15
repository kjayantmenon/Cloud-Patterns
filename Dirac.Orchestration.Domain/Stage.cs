﻿

namespace Dirac.Orchestration.Domain
{

    using System.Collections.Generic;

    public class Stage:ValueObject, IExecutor
    {
        public List<Module> Modules { get; set; }

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