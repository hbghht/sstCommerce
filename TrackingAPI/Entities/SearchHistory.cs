namespace TrackingAPI.Entities
{
    public class SearchHistory
    {
        public Guid RequestId { get; set; }
        
        public string UserName{ get; set; }

        public string Keyword { get; set; }
    }
}