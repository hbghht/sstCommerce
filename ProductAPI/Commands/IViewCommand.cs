using ProductAPI.Entities;
using EventBusRabbitMQ.Events;

namespace ProductAPI.Commands
{
    public interface IViewCommand
    {
        Product Result { get; }
        Task Execute(MessageEvent messageEvent);
    }
}