

namespace Dirac.Orchestration.Domain
{
    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Workflow:Entity, IExecutor
    {
        public class WorkflowFactory
        {
            
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

      

        public int Kill()
        {
            throw new System.NotImplementedException();
        }

        public int Pause()
        {
            throw new System.NotImplementedException();
        }

        ExecutionResult IExecutor.Execute()
        {
            try
            {
                Stages.ForEach(stage => stage.Execute());
            }
            catch (AggregateException ex)
            {

            }
            return 0;
        }

        ExecutionResult IExecutor.Kill()
        {
            throw new NotImplementedException();
        }

        ExecutionResult IExecutor.Pause()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Entity
    {
            int? _requestedHashCode;
            string _Id;
            public virtual string Id
            {
                get
                {
                    return _Id;
                }
                protected set
                {
                    _Id = value;
                }
            }

            private List<INotification> _domainEvents;
            public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

            public void AddDomainEvent(INotification eventItem)
            {
                _domainEvents = _domainEvents ?? new List<INotification>();
                _domainEvents.Add(eventItem);
            }

            public void RemoveDomainEvent(INotification eventItem)
            {
                _domainEvents?.Remove(eventItem);
            }

            public void ClearDomainEvents()
            {
                _domainEvents?.Clear();
            }

            public bool IsTransient()
            {
                return this.Id == default(string);
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is Entity))
                    return false;

                if (Object.ReferenceEquals(this, obj))
                    return true;

                if (this.GetType() != obj.GetType())
                    return false;

                Entity item = (Entity)obj;

                if (item.IsTransient() || this.IsTransient())
                    return false;
                else
                    return item.Id == this.Id;
            }

            public override int GetHashCode()
            {
                if (!IsTransient())
                {
                    if (!_requestedHashCode.HasValue)
                        _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                    return _requestedHashCode.Value;
                }
                else
                    return base.GetHashCode();

            }
            public static bool operator ==(Entity left, Entity right)
            {
                if (Object.Equals(left, null))
                    return (Object.Equals(right, null)) ? true : false;
                else
                    return left.Equals(right);
            }

            public static bool operator !=(Entity left, Entity right)
            {
                return !(left == right);
            }
        }
    }

