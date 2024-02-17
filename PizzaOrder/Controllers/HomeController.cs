using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaOrder.Models;
using Microsoft.AspNetCore.Http;
using Order.Persistence;
namespace PizzaOrder.Controllers
{
    public class HomeController : Controller
    {
        private readonly PizzaOrderContext _context;

        public HomeController(PizzaOrderContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Fav = ( from m in _context.Menu where (m.Count>0) orderby m.Count descending select m).Take(10);
            var categories = (from m in _context.Category select m);
            var HomeVM = new HomeViewModel()
            {
                Menu = Fav.ToList(),
                Category = categories.ToList(),
            };
            return View(HomeVM);
        }
       
            public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
