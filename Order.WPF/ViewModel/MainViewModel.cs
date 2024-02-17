using System;
using System.Collections.Generic;
using System.Linq;
using Order.WPF.Model;
using System.Text;
using System.Threading.Tasks;

namespace Order.WPF.ViewModel
{
    class MainViewModel:ViewModelBase
    {
        private readonly IOrderModel _model;

        public DelegateCommand AddMenuItemCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand OrdersCommand { get; set; }
        public event EventHandler AddMenuItem;
        public event EventHandler Orders;
        public event EventHandler LogoutSuccess;

        public MainViewModel(IOrderModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            AddMenuItemCommand = new DelegateCommand(param => OnAddMenuItem()); 
            OrdersCommand = new DelegateCommand(param => OnOrders());
            LogoutCommand = new DelegateCommand(param => OnLogout());
        }
        private void OnOrders()
        {
            Orders?.Invoke(this, EventArgs.Empty);
        }
        private void OnAddMenuItem()
        {
            AddMenuItem?.Invoke(this, EventArgs.Empty);
        }

        private async void OnLogout()
        {
            try
            {
                await _model.LogoutAsync();
                LogoutSuccess?.Invoke(this, EventArgs.Empty);
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error! ({ex.Message})");
            }
        }
    }
}
