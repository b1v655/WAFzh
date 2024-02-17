using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Order.Persistence.DTO;
using Order.WPF.Model;
using Order.WPF.View;
namespace Order.WPF.ViewModel
{
    class OrdersViewModel : ViewModelBase
    {
        OrdersDTO _selectedOrder;
        String _filter;
        private ObservableCollection<OrdersDTO> _orders;
        private ObservableCollection<MenuDTO> _items;
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand IsAccomplishedCommand { get; set; }
        public DelegateCommand ListingCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand JustAccomplishedCommand { get; set; }
        public DelegateCommand JustNotAccomplishedCommand { get; set; }

        public DelegateCommand FilterNameCommand { get; set; }
        public DelegateCommand FilterAddressCommand { get; set; }
        private readonly IOrderModel _model;

        public ObservableCollection<OrdersDTO> Orders => _orders;
        public event EventHandler Canceled;
        public OrdersViewModel(IOrderModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            LoadData();
            RefreshCommand = new DelegateCommand(param => LoadData());
            JustAccomplishedCommand = new DelegateCommand(param => JustAccomplished());
            JustNotAccomplishedCommand = new DelegateCommand(param => JustNotAccomplished());
            IsAccomplishedCommand = new DelegateCommand(param => OnAccomplished());
            ListingCommand = new DelegateCommand(param => ListingOrderedItems());
            FilterNameCommand = new DelegateCommand(param => FilterName());
            FilterAddressCommand = new DelegateCommand(param => FilterAddress());
            CancelCommand = new DelegateCommand(param => OnCancel());


        }
        private async void OnAccomplished()
        {
            try
            {
                Boolean x = await _model.SendAccomplishedOrder(SelectedOrder);
                LoadData();
            }
            catch (Exception ex)
            {

            }
        }      
        private async void ListingOrderedItems()
        {
            try
            {
                List<int> counts = new List<int> { };
                List<string> list = new List<string> { };
                List<string> listOfNessesseryItems = SelectedOrder.OrderedItems.Substring(0, SelectedOrder.OrderedItems.Length - 1).Split(',').ToList();
                foreach (string str in listOfNessesseryItems)
                {
                    list.Add(str.Split('$')[0].Substring(0, str.Split('$')[0].Length - 1));
                    counts.Add(Int32.Parse(str.Split('$')[1]));
                }
                _items = new ObservableCollection<MenuDTO>(new ObservableCollection<MenuDTO>(await _model.LoadMenu()).Where(o => list.Contains(o.Name)));
                for (int i = 0; i < counts.Count(); i++)
                    _items[i].Count = counts[i];
                new ListingOrderedItems(_items).Show();
            }
            catch(Exception ex)
            {

            }
        }
        public String Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                }
            }
        }
        public OrdersDTO SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged();
                }
            }
        }
        private async void FilterName()
        {
            try
            {
                _orders = new ObservableCollection<OrdersDTO>(await _model.LoadOrders());
                _orders = new ObservableCollection<OrdersDTO>(_orders.Where(o => o.Name == Filter));
                OnPropertyChanged(nameof(Orders));
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }
        private async void FilterAddress()
        {
            try
            {
                _orders = new ObservableCollection<OrdersDTO>(await _model.LoadOrders());
                if(Filter!=null)
                    _orders = new ObservableCollection<OrdersDTO>(_orders.Where(o => o.Address.Contains( Filter)));
                OnPropertyChanged(nameof(Orders));
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }

        }
        private async void JustAccomplished()
        {
            try
            {
                _orders = new ObservableCollection<OrdersDTO>(await _model.LoadOrders());
                _orders = new ObservableCollection<OrdersDTO>(_orders.Where(o => o.OutDate!=DateTime.MinValue));
                OnPropertyChanged(nameof(Orders));
            }
            catch (Exception ex)
            {

            }
        }
        private async void JustNotAccomplished()
        {
            try
            {
                _orders = new ObservableCollection<OrdersDTO>(await _model.LoadOrders());
                _orders = new ObservableCollection<OrdersDTO>(_orders.Where(o => o.OutDate == DateTime.MinValue));
                OnPropertyChanged(nameof(Orders));
            }
            catch (Exception ex)
            {

            }
        }
        private async void LoadData()
        {
            try
            {
                _orders = new ObservableCollection<OrdersDTO>(await _model.LoadOrders());

                OnPropertyChanged(nameof(Orders));
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }
        private void OnCancel()
        {
            Canceled?.Invoke(this, EventArgs.Empty);
        }
    }
}
