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
    public class BasketController: Controller
    {
        private readonly PizzaOrderContext _context;

        public BasketController(PizzaOrderContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            OrderViewModel orderVM = new OrderViewModel
            {
                Food = new List<Tuple<Menu,int>> {  }
            };

            int totalPrice = 0;

            var foods = from m in _context.Menu select m;

            foreach (var key in HttpContext.Session.Keys)
            {
                
                Menu Food = foods.Where(s => s.ID == int.Parse(key)).Single();
                int quantity = HttpContext.Session.GetInt32(key).GetValueOrDefault();
                orderVM.Food.Add(new Tuple<Menu,int>(Food,quantity));
                totalPrice += Food.Price*quantity;
            }
            orderVM.FullPrice = totalPrice;
            return View(orderVM);
        }
        public IActionResult EditBasket( string foodID , int deincrease=0)
        {
            OrderViewModel orderVM = new OrderViewModel
            {
                FailNumber=0,
                Food = new List<Tuple<Menu, int>> { }
            };

            int totalPrice = 0;

            var foods = from m in _context.Menu select m;

            foreach (var key in HttpContext.Session.Keys)
            {

                Menu Food = foods.Where(s => s.ID == int.Parse(key)).Single();
                int quantity = HttpContext.Session.GetInt32(key).GetValueOrDefault();
                
                totalPrice += Food.Price * quantity;
            }
            
            if (deincrease == 1)
            {
                if (HttpContext.Session.GetInt32(foodID).GetValueOrDefault() > 1)
                {
                    HttpContext.Session.SetInt32(foodID, HttpContext.Session.GetInt32(foodID).GetValueOrDefault() - 1);
                }
                else if (HttpContext.Session.GetInt32(foodID).GetValueOrDefault() == 1)
                    HttpContext.Session.Remove(foodID);
            }
            else if (deincrease == 2)
            {


                if (totalPrice + foods.Where(s => s.ID == int.Parse(foodID)).Single().Price <= 20000)
                {
                    HttpContext.Session.SetInt32(foodID, HttpContext.Session.GetInt32(foodID).GetValueOrDefault() + 1);
                }
                else
                    orderVM.FailNumber = 1;
            }
            else if (deincrease == 0)
                HttpContext.Session.Remove(foodID);
            totalPrice = 0;
            foreach (var key in HttpContext.Session.Keys)
            {

                Menu Food = foods.Where(s => s.ID == int.Parse(key)).Single();
                int quantity = HttpContext.Session.GetInt32(key).GetValueOrDefault();
                orderVM.Food.Add(new Tuple<Menu, int>(Food, quantity));
                totalPrice += Food.Price * quantity;
            }
            orderVM.FullPrice = totalPrice;
            return View("index",orderVM);
            
        }

        public IActionResult BasketClear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var foods = from m in _context.Menu select m;
                List<Menu> ProductsList = new List<Menu> { };
                string orderedItem = "";
                foreach (var key in HttpContext.Session.Keys)
                {
                    ProductsList.Add(foods.Where(s => s.ID == int.Parse(key)).Single());
                    foods.Where(s => s.ID == int.Parse(key)).Single().Count += (int) HttpContext.Session.GetInt32(key);
                    orderedItem += foods.Where(s => s.ID == int.Parse(key)).Single().Name + " $" + (int)HttpContext.Session.GetInt32(key) + ",";
                }
                int totalPrice = 0;
                foreach (var key in HttpContext.Session.Keys)
                {

                    Menu Food = foods.Where(s => s.ID == int.Parse(key)).Single();
                    int quantity = HttpContext.Session.GetInt32(key).GetValueOrDefault();
                    totalPrice += Food.Price * quantity;
                }
                Orders order = new Orders
                {
                    Name = orderVM.Name,
                    OrderDate = DateTime.Now,
                    Address = orderVM.Address,
                    PhoneNumber = orderVM.PhoneNumber,
                    OrderedItems = orderedItem,
                    FullPrice = totalPrice
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                HttpContext.Session.Clear();
                return View("Resoult");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
