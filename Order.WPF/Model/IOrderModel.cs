using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Persistence.DTO;

namespace Order.WPF.Model
{
    public interface IOrderModel
    {
        Task<IEnumerable<CategoryDTO>> LoadCategories();
        bool IsUserLoggedIn { get; }
        Task<bool> LoginAsync(string name, string password);
        Task<bool> LogoutAsync();
        Task<IEnumerable<OrdersDTO>> LoadOrders();
        Task<IEnumerable<MenuDTO>> LoadMenu();
        Task<Boolean> SendNewMenuItem(MenuDTO data);
        Task<Boolean> SendAccomplishedOrder(OrdersDTO data);
    }
}
