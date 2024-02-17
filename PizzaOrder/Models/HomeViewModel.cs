using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Persistence;

namespace PizzaOrder.Models
{
    public class HomeViewModel
    {
        public List<Category> Category;
        public List<Menu> Menu;
        public int FailNumber;
        public int ActualCategory;
        public String Filter;

    }
}
