using TrackingAPI.Data;
using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public class FilterHistoryRepository : IFilterHistoryRepository
    {
        private readonly ITrackingContext _context;

        public FilterHistoryRepository(ITrackingContext trackingContext)
        {
            _context = trackingContext ?? throw new ArgumentNullException(nameof(trackingContext));
        }
        
        public async Task Insert(FilterHistory filterHistory)
        {
            await _context.FilterHistories.InsertOneAsync(filterHistory);
        }
    }
}