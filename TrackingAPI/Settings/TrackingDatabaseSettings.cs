namespace TrackingAPI.Settings
{
    public class TrackingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SearchCollectionName { get; set; }
        public string FilterCollectionName { get; set; }
        public string ViewCollectionName { get; set; }
    }
}