using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public interface IViewHistoryRepository
    {
         Task Insert(ViewHistory viewHistory);
    }
}