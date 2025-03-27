using HotelMiamiApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelMiamiApp.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var user = new User("Vasya", "Pupkin", "admin", "1234", "Администратор");
            if(LoginTextBox.Text.Equals("") || LoginPasswordBox.Password.Equals(""))
            {
                MessageBox.Show("Не все поля заполнены!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (LoginTextBox.Text.Equals(user.Login) && LoginPasswordBox.Password.Equals(user.Password)) {
                MessageBox.Show("Добро пожаловать!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                AdministratorWindow window = new AdministratorWindow();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль! Попробуйте ещё раз!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
