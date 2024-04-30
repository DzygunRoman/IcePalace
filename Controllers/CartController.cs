using AcePalace.Areas.Identity.Data;
using AcePalace.Models.DTO;
using AcePalace.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcePalace.Controllers
{
   
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AcePalaceContext _context;
        public CartController(ICartRepository cartRepo, IWebHostEnvironment webHostEnvironment, AcePalaceContext context)
        {
            _cartRepo = cartRepo;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
       
        public async Task<IActionResult> AddItem(int productID, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(productID, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int productID)
        {
            var cartCount = await _cartRepo.RemoveItem(productID);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            
            return View(cart);
        }

        public  async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public  IActionResult Checkout()
        {
            var Shedules = new CheckoutModel
            {
                Shedules = _context.Shedules.OrderBy(s => s.DateTime).ToList()
            };
            return View(Shedules);
          
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await _cartRepo.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
