using MassTransit;
using EventBusRabbitMQ.Events;
using TrackingAPI.Repositories;
using TrackingAPI.Entities;

namespace TrackingAPI.Consumers
{
    public class ProductSearchConsumer : IConsumer<ProductSearchEvent>
    {
        private readonly ISearchHistoryRepository _repository; 

        public ProductSearchConsumer(ISearchHistoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Consume(ConsumeContext<ProductSearchEvent> context)
        {
            var data = context.Message;
            Console.WriteLine(data.Keyword);
            SearchHistory searchHistory = new SearchHistory{
                Keyword = data.Keyword,
                RequestId = data.RequestId,
                UserName = data.UserName
            };

            await _repository.Insert(searchHistory);
        }
    }
}