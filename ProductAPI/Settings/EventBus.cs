namespace ProductAPI.Settings
{
    public class EventBus
    {
        public string HostName { get; set; } = "rabbitmq://localhost";

        public string UserName { get; set; } = "guest";

        public string Password { get; set; } = "guest";
    }
}