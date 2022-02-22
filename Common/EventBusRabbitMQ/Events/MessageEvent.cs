namespace EventBusRabbitMQ.Events
{
    public class MessageEvent
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; }
    }
}