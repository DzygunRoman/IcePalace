using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcePalace.Areas.Identity.Data;
using AcePalace.Models;

namespace AcePalace.Controllers
{
    public class ShedulesUserController : Controller
    {
        private readonly AcePalaceContext _context;

        public ShedulesUserController(AcePalaceContext context)
        {
            _context = context;
        }        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shedules.OrderBy(s => s.DateTime).ToListAsync());
        }

       
        

       
    }
}
