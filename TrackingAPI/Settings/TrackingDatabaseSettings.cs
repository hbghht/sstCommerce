namespace TrackingAPI.Settings
{
    public class TrackingDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "TrackingDb";
        public string SearchCollectionName { get; set; } = "SearchHistories";
        public string FilterCollectionName { get; set; } = "FilterHistories";
        public string ViewCollectionName { get; set; } = "ViewHistories";
    }
}