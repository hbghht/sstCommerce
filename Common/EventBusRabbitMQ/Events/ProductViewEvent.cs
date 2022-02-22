namespace EventBusRabbitMQ.Events
{
    public class ProductViewEvent : MessageEvent
    {
        public int Id { get; set; }
    }
}