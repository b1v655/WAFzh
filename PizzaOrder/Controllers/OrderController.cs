using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using Microsoft.AspNetCore.Http;
using Order.Persistence;
namespace PizzaOrder.Controllers
{
    public class OrderController : Controller
    {
        private readonly PizzaOrderContext _context;
        public OrderController(PizzaOrderContext context)
        {
            _context = context;
        }
        public IActionResult Index(int category, String Filter = "", string foodID=null)
        {

            var menu = from m in _context.Menu where category == m.Category.ID && (m.Name.Contains(Filter)) select m;
            if (Filter == null)
            {
                menu = from m in _context.Menu where category == m.Category.ID select m;
            }
            var categories = from m in _context.Category select m;
            var foods = from m in _context.Menu  select m;
            byte fail = 0;
            int totalPrice = 0;
            foreach(var key in HttpContext.Session.Keys)
            {
                Menu Food = foods.Where(s => s.ID == int.Parse(key)).Single();
                int quantity = HttpContext.Session.GetInt32(key).GetValueOrDefault();
                
                totalPrice += Food.Price * quantity;
            }

            if (foodID != null && HttpContext.Session.GetInt32(foodID) == null)
            {
                if (totalPrice + foods.Where(s => s.ID == int.Parse(foodID)).Single().Price <= 20000)
                {
                    HttpContext.Session.SetInt32(foodID, 1);
                    fail = 2;

                }
                else
                    fail = 1;
            }
            else if (foodID != null)
                if (totalPrice + foods.Where(s => s.ID == int.Parse(foodID)).Single().Price <= 20000)
                {
                    HttpContext.Session.SetInt32(foodID, (int)HttpContext.Session.GetInt32(foodID) + 1);
                    fail = 2;
                }
                else
                    fail = 1;


            var OrderIndexVM = new HomeViewModel()
            {
                FailNumber = fail,
                Category = categories.ToList(),
                Menu = menu.ToList(),
                ActualCategory = category,
                Filter = Filter
            };
            return View(OrderIndexVM);


        }


    }
}
