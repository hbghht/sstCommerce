namespace EventBusRabbitMQ.Events
{
    public class ProductFilterEvent : MessageEvent
    {
        public string Column { get; set; }

        public string Value { get; set; }
    }
}