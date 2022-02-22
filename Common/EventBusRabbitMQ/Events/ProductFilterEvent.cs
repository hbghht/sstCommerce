namespace EventBusRabbitMQ.Events
{
    public class ProductFilterEvent : MessageEvent
    {
        public string Column { get; set; } = String.Empty;

        public string Value { get; set; } = String.Empty;
    }
}