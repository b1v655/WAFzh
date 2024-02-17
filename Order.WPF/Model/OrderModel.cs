using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Order.Persistence.DTO;
using System.Net;

namespace Order.WPF.Model
{
    class OrderModel : IOrderModel
    {
        private readonly HttpClient _client;

        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn => _isUserLoggedIn;
        public OrderModel(string baseAddress)
        {
            _isUserLoggedIn = false;
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
        


        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                _isUserLoggedIn = true;
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task<bool> LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Signout", "");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task<Boolean> SendAccomplishedOrder(OrdersDTO newData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Orders", newData);

            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<CategoryDTO>> LoadCategories()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Category/CategoryList"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>();
                }

                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        public async Task<IEnumerable<OrdersDTO>> LoadOrders()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Orders/OrdersList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<OrdersDTO>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }
        public async Task<IEnumerable<MenuDTO>> LoadMenu()
        {
            HttpResponseMessage res = await _client.GetAsync("api/Menu/MenuList");

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<IEnumerable<MenuDTO>>();
            }

            throw new NetworkException("Service returned response: " + res.StatusCode);
        }
        public async Task<Boolean> SendNewMenuItem(MenuDTO newData)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Menu", newData);

            return response.IsSuccessStatusCode;
        }

        
    }
}
