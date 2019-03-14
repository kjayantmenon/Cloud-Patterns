

namespace Dirac.Orchestration.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Workflow:Entity
    {
        public class WorkflowFactory
        {
            public static Workflow CreateWorkflow()
            {
                return new Workflow()
                {
                    Id = "not-set"
                };
            }

            public static Workflow GetWorkflow(string id)
            {
                return new Workflow()
                {
                    Id = id
                };
            }



        }



        List<Stage> _stages = null;
        public List<Stage> Stages {
            get { return _stages; }
        }


        public Workflow()
        {
            _stages = new List<Stage>();
        }

        protected Workflow(string id)
        {
            this.Id = id;
        }
        public async Task AddStage(Stage stage)
        {

        }

    }
}
