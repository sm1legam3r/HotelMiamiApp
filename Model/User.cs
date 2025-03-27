using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMiamiApp.Model
{
    public class User
    {
        private int userId;
        private string name;
        private string surname;
        private string login;
        private string password;
        private string role;
        private bool isBlocked;
        private DateTime lastEnterDate;

        public User(string name, string surname, string login, string password, string role)
        {
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public int GetUserId() { 
            return userId;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }
        public string Login
        {
            get { return this.login; }
            set { this.surname = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.surname = value; }
        }

        public string Role
        {
            get { return this.role; }
        }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }
    }
}