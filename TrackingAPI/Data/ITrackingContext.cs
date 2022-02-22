using TrackingAPI.Entities;
using MongoDB.Driver;

namespace TrackingAPI.Data
{
    public interface ITrackingContext
    {
        IMongoCollection<SearchHistory> SearchHistories { get; }

        IMongoCollection<FilterHistory> FilterHistories { get; }

        IMongoCollection<ViewHistory> ViewHistories { get; }
    }
}
