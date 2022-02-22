using MassTransit;
using EventBusRabbitMQ.Events;
using TrackingAPI.Repositories;
using TrackingAPI.Entities;

namespace TrackingAPI.Consumers
{
    public class ProductViewConsumer : IConsumer<ProductViewEvent>
    {
        private readonly IViewHistoryRepository _repository; 

        public ProductViewConsumer(IViewHistoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Consume(ConsumeContext<ProductViewEvent> context)
        {
            var data = context.Message;
            Console.WriteLine(data.Id);
            var viewHistory = new ViewHistory{
                ProductId = data.Id,
                RequestId = data.RequestId,
                UserName = data.UserName
            };

            await _repository.Insert(viewHistory);
        }
    }
}