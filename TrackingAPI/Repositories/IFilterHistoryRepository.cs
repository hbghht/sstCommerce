using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public interface IFilterHistoryRepository
    {
        Task Insert(FilterHistory filterHistory);
    }
}