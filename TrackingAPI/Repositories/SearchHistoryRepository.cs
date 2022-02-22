using TrackingAPI.Data;
using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly ITrackingContext _context;

        public SearchHistoryRepository(ITrackingContext trackingContext)
        {
            _context = trackingContext ?? throw new ArgumentNullException(nameof(trackingContext));
        }
        
        public async Task Insert(SearchHistory searchHistory)
        {
            await _context.SearchHistories.InsertOneAsync(searchHistory);
        }
    }
}