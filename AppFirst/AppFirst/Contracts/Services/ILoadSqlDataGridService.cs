using AppFirst.Models;

namespace AppFirst.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ILoadSqlDataGridService
{
    Task<IEnumerable<Order>> GetContentGridDataAsync();

    Task<IEnumerable<Order>> GetGridDataAsync();

    Task<IEnumerable<Order>> GetListDetailsDataAsync();
}
