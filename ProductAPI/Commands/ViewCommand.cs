using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Common;
using ProductAPI.Repositories;
using ProductAPI.Entities;
using MassTransit;
using ProductAPI.Settings;

namespace ProductAPI.Commands
{
    public class ViewCommand: IViewCommand
    {
        private readonly IProductRepository _repository;

        public Product Result{get; private set;}

        private readonly ILogger<SearchCommand> _logger;

        private readonly EventBus _eventBus;

        private readonly IBus _bus;

        public ViewCommand(IProductRepository repository, IBus bus, EventBus eventBus, ILogger<SearchCommand> logger)
        {
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
            _bus = bus?? throw new ArgumentNullException(nameof(bus));
            _eventBus = eventBus?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger;
        }

        public async Task Execute(MessageEvent messageEvent)
        {
            var viewEvent = messageEvent as ProductViewEvent;
            if(viewEvent != null)
            {
                Result = await _repository.GetProduct(viewEvent.Id);
                
                try
                {
                    var uri = new Uri(_eventBus.HostName); 
                    var relativeUri = new Uri(EventBusConstants.ProductViewQueue, UriKind.Relative);
                    var endPoint = await _bus.GetSendEndpoint(new Uri(uri, relativeUri));
                    await endPoint.Send(viewEvent); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", viewEvent.RequestId, "Product");
                    throw;
                }
            }
        }
    }
}