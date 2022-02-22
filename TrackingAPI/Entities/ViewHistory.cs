namespace TrackingAPI.Entities
{
    public class ViewHistory
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; } = String.Empty;

        public int ProductId { get; set; }
    }
}