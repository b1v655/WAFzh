using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Persistence;
using Order.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Order.WebApi.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly PizzaOrderContext _context;
        public OrdersController(PizzaOrderContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> IsAccomplished([FromBody] OrdersDTO newData)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var orders = _context.Orders.Where(o => newData.ID == o.ID);
                    foreach (var order in orders)
                    {
                        if (order.OutDate == DateTime.MinValue)
                        {
                            order.OutDate = DateTime.Now;
                        }
                            
                    }

                    _context.UpdateRange(orders);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                BadRequest();
            }
            return Ok();
        }
        [Authorize]
        [HttpGet("OrdersList")]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Orders.ToList().Select(OrdersItem => new OrdersDTO
                {
                    ID = OrdersItem.ID,
                    Name = OrdersItem.Name,
                    Address = OrdersItem.Address,
                    PhoneNumber = OrdersItem.PhoneNumber,
                    OrderDate = OrdersItem.OrderDate,
                    OrderedItems =  OrdersItem.OrderedItems,
                    OutDate = OrdersItem.OutDate,
                    FullPrice = OrdersItem.FullPrice

                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}