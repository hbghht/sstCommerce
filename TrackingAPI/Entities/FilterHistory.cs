namespace TrackingAPI.Entities
{
    public class FilterHistory
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; }

        public string Column { get; set; }
        
        public string Value { get; set; }
    }
}