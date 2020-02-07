namespace Common.Q
{
    using System.Threading.Tasks;

    public interface IQClient
    {
        public Task PublishAsync(string message);
        public Task PublishAsync(string hostName, string qName, string port, string message);
    }
}
