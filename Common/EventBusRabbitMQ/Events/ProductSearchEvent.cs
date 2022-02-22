namespace EventBusRabbitMQ.Events
{
    public class ProductSearchEvent : MessageEvent
    {
        public string Keyword { get; set; } = String.Empty;
    }
}