namespace TrackingAPI.Entities
{
    public class SearchHistory
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; } = String.Empty;

        public string Keyword { get; set; } = String.Empty;
    }
}