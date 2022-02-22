namespace TrackingAPI.Entities
{
    public class FilterHistory
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; } = String.Empty;

        public string Column { get; set; } = String.Empty;
        
        public string Value { get; set; } = String.Empty;
    }
}