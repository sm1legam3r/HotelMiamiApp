using HotelMiamiApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        bool success = false;
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        Database database = new Database();

        private void RegistrateButtonClick(object sender, RoutedEventArgs e)
        {
            success = RegistraionSuccess() && CheckData() && CheckDatabase();
            if (success)
            {


                AddUserToDatabase();

                MessageBox.Show("Регистрация успешна!");
                this.Close();
            }
        }

        private void AddUserToDatabase()
        {
            var surname = SurnameTextBox.Text;
            var name = NameTextBox.Text;
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Password;
            var role = RoleComboBox.SelectedIndex == 0 ? "Пользователь" : "Администратор";
            var date = DateTime.Now;

            string query = "INSERT INTO users(surname, name, login, password, role, is_blocked, last_enter_date) " +
               $"VALUES('{surname}','{name}','{login}','{password}','{role}','false','{date}')";
            try
            {
                SqlCommand command = new SqlCommand(query, database.GetSqlConnection());
                database.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные успешно добавлены в БД", "Инфомация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка"
                    , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool RegistraionSuccess()
        {
            var formFullness = NameTextBox.Text.Equals("") && SurnameTextBox.Text.Equals("") && LoginTextBox.Text.Equals("") && PasswordTextBox.Password.Equals("");

            if (!formFullness)
            {
                return true;

            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


        }

        private bool CheckData()
        {
            var forbiddenSymbols = !Regex.IsMatch(SurnameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(NameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(LoginTextBox.Text, @"^[a-zA-Z\s]*$");

            if (forbiddenSymbols)
            {
                MessageBox.Show("Поля фамилия, имя или логин содержат запрещенные символы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckDatabase()
        {
            var login = LoginTextBox.Text;
            string query = $"SELECT login FROM users WHERE login = '{login}'";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();



            try
            {
                SqlCommand command = new SqlCommand(query, database.GetSqlConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}