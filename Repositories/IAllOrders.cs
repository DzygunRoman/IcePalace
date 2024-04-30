using AcePalace.Models;

namespace AcePalace.Repositories
{
    public interface IAllOrders
    {
        Task<IEnumerable<Order>> createOrder();
    }
}
