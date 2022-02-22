using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Common;
using ProductAPI.Repositories;
using ProductAPI.Entities;
using MassTransit;
using ProductAPI.Settings;

namespace ProductAPI.Commands
{
    public class SearchCommand : IQueryCommand
    {
        private readonly IProductRepository _repository;

        public IEnumerable<Product> Result{get; private set;}

        private readonly ILogger<SearchCommand> _logger;

        private readonly EventBus _eventBus;

        private readonly IBus _bus;

        public SearchCommand(IProductRepository repository, IBus bus, EventBus eventBus, ILogger<SearchCommand> logger)
        {
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
            _bus = bus?? throw new ArgumentNullException(nameof(bus));
            _eventBus = eventBus?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger;
            Result = new List<Product>();
        }

        public async Task Execute(MessageEvent messageEvent)
        {
            var searchEvent = messageEvent as ProductSearchEvent;
            if(searchEvent != null)
            {
                Result = await _repository.GetProductByName(searchEvent.Keyword);
                
                try
                {
                    var uri = new Uri(_eventBus.HostName); 
                    var relativeUri = new Uri(EventBusConstants.ProductSearchQueue, UriKind.Relative);
                    var endPoint = await _bus.GetSendEndpoint(new Uri(uri, relativeUri));
                    await endPoint.Send(searchEvent); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", searchEvent.RequestId, "Product");
                    throw;
                }
            }
        }
    }
}