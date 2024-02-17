using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using Order.WPF.Model;
using Order.Persistence.DTO;
namespace Order.WPF.ViewModel
{
    class AddMenuItemViewModel : ViewModelBase
    {
        private readonly IOrderModel _model;
        private MenuDTO _newMenuItem;
        public ObservableCollection<CategoryDTO> _categories;
        
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public ObservableCollection<CategoryDTO> Categories
        {
            get { return _categories; }
            private set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }
        public event EventHandler Success;
        public event EventHandler Canceled;

        public AddMenuItemViewModel(IOrderModel model)
        {
            _model = model;

            _newMenuItem = new MenuDTO();
            LoadData();
            SendCommand = new DelegateCommand(param => AddNewMenuItem());
            CancelCommand = new DelegateCommand(param => OnCancel());
            
        }

        private async void LoadData()
        {
            try
            {
                
                Categories = new ObservableCollection<CategoryDTO>(await _model.LoadCategories());
                
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }
        public MenuDTO NewMenuItem
        {
            get => _newMenuItem;
            set
            {
                _newMenuItem = value;
                OnPropertyChanged();
            }
        }


        

        private async void AddNewMenuItem()
        {
            if (!CheckModel()) { return; }
            if (await _model.SendNewMenuItem(NewMenuItem))
            {
                OnSuccessfulAdd();
            }
            else
            {
                OnMessageApplication("Nem sikerült hozzáadni a listához.");
            }
        }

        private void OnCancel()
        {
            Canceled?.Invoke(this, EventArgs.Empty);
        }

        private void OnSuccessfulAdd()
        {
            Success?.Invoke(this, EventArgs.Empty);
        }

        private Boolean CheckModel()
        {
            if (NewMenuItem.Name == null)
            {
                OnMessageApplication("Nem adta meg a nevet!");
                return false;
            }
            if (NewMenuItem.Price.ToString() == null)
            {
                OnMessageApplication("Nem adott meg árat!");
                return false;
            }
            if (NewMenuItem.CategoryID.ToString() == null)
            {
                OnMessageApplication("Nem adott meg kategóriát!");
                return false;
            }
            return true;
        }
    }
}
