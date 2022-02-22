using ProductAPI.Entities;
using EventBusRabbitMQ.Events;

namespace ProductAPI.Commands
{
    public interface IQueryCommand
    {
        public IEnumerable<Product> Result { get; }
        Task Execute(MessageEvent messageEvent);
    }
}
