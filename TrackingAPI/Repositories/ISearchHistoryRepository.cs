using TrackingAPI.Entities;

namespace TrackingAPI.Repositories
{
    public interface ISearchHistoryRepository
    {
        Task Insert(SearchHistory searchHistory);
    }
}