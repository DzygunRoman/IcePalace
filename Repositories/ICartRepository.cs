using AcePalace.Models;
using AcePalace.Models.DTO;

namespace AcePalace.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int ProductID, int qty);
        Task<int> RemoveItem(int ProductID);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
