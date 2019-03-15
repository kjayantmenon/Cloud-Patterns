
namespace Dirac.Orchestration.Domain
{
    public interface IExecutor
    {
        ExecutionResult Execute();
        ExecutionResult Kill();
        ExecutionResult Pause();
    }

    public enum ExecutionResult { Success=1, Failure=0, InProgress=2, Suspended=3, Unknown=9}
}
