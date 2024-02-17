using System;
using System.Collections.Generic;
using System.Linq;
using Order.Persistence;
using Order.Persistence.DTO;
using Order.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Order.WebApi.Test
{
    public class OrderTest : IDisposable
    {
        private readonly PizzaOrderContext _context;
        private readonly List<MenuDTO> _MenuDTO;
        private readonly List<OrdersDTO> _OrdersDTO;
        private readonly List<CategoryDTO> _CategoryDTO;
        public OrderTest()
        {
            var options = new DbContextOptionsBuilder<PizzaOrderContext>()
                .UseInMemoryDatabase("PizzaOrderTest")
                .Options;

            _context = new PizzaOrderContext(options);
            _context.Database.EnsureCreated();
            var categoryData = new List<Category>
            {
                new Category
                {
                    Name = "TESTFOOD",
                },
                 new Category
                {
                    Name = "TESTFOOD2",
                }
            };
            _context.Category.AddRange(categoryData);

            var menuData = new List<Menu>
            {
                new Menu
                {
                    Name = "TEST_FOOD_1",
                    Description = "Tasty.",
                    Category = categoryData[0],
                    Price=1000,
                    Spicy=false,
                    Count=1,
                    Vegetarian=false

                },
                new Menu
                {
                    Name = "TEST_FOOD_1",
                    Description = "Tasty.",
                    Category = categoryData[0],
                    Price=1000,
                    Spicy=false,
                    Count=1,
                    Vegetarian=false

                },
                new Menu
                {
                    Name = "TEST_FOOD_1",
                    Description = "Tasty.",
                    Category = categoryData[0],
                    Price=1000,
                    Count=1,
                    Spicy=false,
                    Vegetarian=false

                },
            };
            _context.Menu.AddRange(menuData);

            var ordersData = new List<Orders>
            {
                new Orders
                {
                    Name="TESTELEK",
                    Address="TEST_STREET",
                    PhoneNumber="06303416278",
                    OrderedItems= "TEST_FOOD_1 x1, TEST_FOOD_2 x1, TEST_FOOD_3 x1,",
                    FullPrice=3000,
                    OrderDate=DateTime.Now,
                    OutDate=DateTime.MinValue
                }
            };
            _context.Orders.AddRange(ordersData);
            _context.SaveChanges();

            _MenuDTO = menuData.Select(menu => new MenuDTO
            {
                ID = menu.ID,
                Category = _context.Category.Single(o => o.Name == "TESTFOOD")
            }).ToList();
            _CategoryDTO = categoryData.Select(category => new CategoryDTO
            {
                ID = category.ID,
                Name = category.Name

            }).ToList();
            _OrdersDTO = ordersData.Select(order => new OrdersDTO
            {
                ID = order.ID,
                Name = order.Name,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                OrderedItems = order.OrderedItems,
                FullPrice = order.FullPrice,
                OrderDate = order.OrderDate,
                OutDate = order.OutDate

            }).ToList();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        
        [Fact]

        public void GetOrdersTest()
        {
            var controller = new OrdersController(_context);
            var result = controller.List();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<OrdersDTO>>(objectResult.Value);
            Assert.Equal(model, _OrdersDTO);
        }
        [Fact]

        public void GetMenuItemsTest()
        {
            var controller = new MenuController(_context);
            var result = controller.List();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MenuDTO>>(objectResult.Value);
            Assert.Equal(model, _MenuDTO);
        }
        [Fact]
        public void GetCategoryTest()
        {
            var controller = new CategoryController(_context);
            var result = controller.List();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(objectResult.Value);
            
            Assert.Equal(_CategoryDTO, model);
        }

        [Fact]
        public  void AddNewMenuItemTest()
        {
            var newMenuItem = new MenuDTO
            {
                Name = "TEST_FOOD_4",
                Category = _context.Category.Single(o => o.Name == "TESTFOOD"),
                Description = "TEST_FOOD_4",
                Price = 1000,
                Spicy = true,
                Vegetarian = false,
            };

            var controller = new MenuController(_context);
            var result =  controller.AddNewMenuItem(newMenuItem);

            var objectResult =  Assert.IsType<OkResult>(result);

            Assert.Equal(_MenuDTO.Count +1, _context.Menu.Count());
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void IsAccomplishedTest()
        {
            var controller = new OrdersController(_context);
            Assert.Equal(DateTime.MinValue, _context.Orders.Last(o => o.Name == "TESTELEK").OutDate);
            var result = await controller.IsAccomplished(_OrdersDTO[0]);
            var objectResult = Assert.IsType<OkResult>(result);
            Assert.NotEqual(DateTime.MinValue, _context.Orders.Last(o => o.Name == "TESTELEK").OutDate);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
