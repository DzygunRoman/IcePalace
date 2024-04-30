using AcePalace.Areas.Identity.Data;
using AcePalace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
namespace AcePalace.Repositories
{
    public class OrdersRepository : IAllOrders
    {
        private readonly AcePalaceContext _appDBContext;        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersRepository(AcePalaceContext appDBContext,UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _appDBContext = appDBContext;           
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IEnumerable<Order>> createOrder()
        {
            var userId = GetUserId();
            

           // if (string.IsNullOrEmpty(userId))
           //     throw new Exception("User is not logged-in");
            var orders = await _appDBContext.Order                            
                            .Include(x => x.OrderDetail)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)                            
                            .Where(a => a.UserId == userId)
                            .ToListAsync();
            return orders;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
