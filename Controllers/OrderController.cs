using AcePalace.Models;
using AcePalace.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AcePalace.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders _allOrders;
       

        public OrderController(IAllOrders allOrders)
        {
            _allOrders = allOrders;
            
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _allOrders.createOrder();
            return View(orders);
        }
       
    }
}
