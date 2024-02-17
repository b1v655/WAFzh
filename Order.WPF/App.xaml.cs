using System;
using System.Configuration;
using System.Windows;

using Order.WPF.Model;
using Order.WPF.View;
using Order.WPF.ViewModel;

namespace Order.WPF
{
    public partial class App : Application
    {
        private IOrderModel _model;
        private LoginViewModel _loginViewModel;
        private MainViewModel _mainViewModel;
        private LoginWindow _loginWindow;
        private MainWindow _mainWindow;
        private OrdersViewModel _ordersViewModel;
        private OrdersWindow _ordersWindow;
        private AddMenuItemViewModel _addMenuItemViewModel;
        private AddMenuItemWindow _addMenuItemWindow;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new OrderModel(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_model);
            _loginViewModel.ExitApplication += ViewModel_ExitApplication;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;
            _loginViewModel.LoginSuccess += ViewModel_LoginSuccess;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;

            _loginWindow = new LoginWindow
            {
                DataContext = _loginViewModel
            };
            _loginWindow.Show();
        }

        private void ViewModel_ExitApplication(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void ViewModel_LoginSuccess(object sender, EventArgs e)
        {
            OpenMenu(_loginWindow);
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            ShowMsgBox("Rossz felhasználónév vagy jelszó!", "Bejelentkezés");
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            ShowMsgBox(e.Message);
        }
        private void ShowMsgBox(String msg, String source = "")
        {
            MessageBox.Show(msg, source, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        private void ViewModel_Logout(object sender, EventArgs e)
        {
            _loginWindow = new LoginWindow
            {
                DataContext = _loginViewModel
            };
            _loginViewModel = new LoginViewModel(_model);

            _loginViewModel.ExitApplication += ViewModel_ExitApplication;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;
            _loginViewModel.LoginSuccess += ViewModel_LoginSuccess;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginWindow.Show();
            _mainWindow.Close();
        }
        private void OpenMenu(Window previous = null)
        {
            _mainViewModel = new MainViewModel(_model);
            _mainViewModel.AddMenuItem += OpenAddMenuItem;
            _mainViewModel.Orders += OpenOrders;
            _mainViewModel.LogoutSuccess += ViewModel_Logout;
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;

            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
            previous?.Close();
            _mainWindow.Show();
        }
        private void OpenAddMenuItem(object sender, EventArgs e)
        {
            _addMenuItemViewModel = new AddMenuItemViewModel(_model);
            _addMenuItemViewModel.Canceled += (o, args) => { OpenMenu(_addMenuItemWindow); };
            _addMenuItemViewModel.MessageApplication += ViewModel_MessageApplication;
            _addMenuItemViewModel.Success += (o, args) =>
            {
                OpenMenu(_addMenuItemWindow);
                ShowMsgBox("Sikeresen hozzáadva.");
            };

            _addMenuItemWindow = new AddMenuItemWindow { DataContext = _addMenuItemViewModel };

            _addMenuItemWindow.Show();
            _mainWindow.Close();
        }
        private void OpenOrders(object sender, EventArgs e)
        {
            _ordersViewModel = new OrdersViewModel(_model);
            _ordersViewModel.Canceled += (o, args) => { OpenMenu(_ordersWindow); };
            _ordersViewModel.MessageApplication += ViewModel_MessageApplication;
           

            _ordersWindow = new OrdersWindow { DataContext = _ordersViewModel };

            _ordersWindow.Show();
            _mainWindow.Close();
        }
    }
}