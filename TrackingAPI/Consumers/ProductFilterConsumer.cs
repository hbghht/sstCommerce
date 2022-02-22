using MassTransit;
using EventBusRabbitMQ.Events;
using TrackingAPI.Repositories;
using TrackingAPI.Entities;

namespace TrackingAPI.Consumers
{
    public class ProductFilterConsumer : IConsumer<ProductFilterEvent>
    {
        private readonly IFilterHistoryRepository _repository; 

        public ProductFilterConsumer(IFilterHistoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Consume(ConsumeContext<ProductFilterEvent> context)
        {
            var data = context.Message;
            Console.WriteLine(data.Value);
            var filterHistory = new FilterHistory{
                Column = data.Column,
                Value = data.Value,
                RequestId = data.RequestId,
                UserName = data.UserName
            };

            await _repository.Insert(filterHistory);
        }
    }
}