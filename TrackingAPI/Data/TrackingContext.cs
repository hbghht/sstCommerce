using MongoDB.Driver;
using TrackingAPI.Entities;
using TrackingAPI.Settings;

namespace TrackingAPI.Data
{
    public class TrackingContext : ITrackingContext
    {
        public IMongoCollection<SearchHistory> SearchHistories { get; }

        public IMongoCollection<FilterHistory> FilterHistories { get; }

        public IMongoCollection<ViewHistory> ViewHistories { get; }

        public TrackingContext(TrackingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            SearchHistories = database.GetCollection<SearchHistory>(settings.SearchCollectionName);
            FilterHistories = database.GetCollection<FilterHistory>(settings.FilterCollectionName);
            ViewHistories = database.GetCollection<ViewHistory>(settings.ViewCollectionName);
        }
    }
}