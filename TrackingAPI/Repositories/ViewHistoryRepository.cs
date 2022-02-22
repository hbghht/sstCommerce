using TrackingAPI.Data;
using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public class ViewHistoryRepository : IViewHistoryRepository
    {
        private readonly ITrackingContext _context;

        public ViewHistoryRepository(ITrackingContext trackingContext)
        {
            _context = trackingContext ?? throw new ArgumentNullException(nameof(trackingContext));
        }
        
        public async Task Insert(ViewHistory viewHistory)
        {
            await _context.ViewHistories.InsertOneAsync(viewHistory);
        }
    }
}