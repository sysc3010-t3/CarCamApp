using System;
using System.Collections.Generic;
using System.Text;

namespace CarCamApp.Models
{
    public class User
    {
        private object entry_Username;

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public bool CheckInformation()
        {
            if (this.Username.Equals("") && this.Password.Equals(""))
                return false;
            else
                return true;
        }
    }
}
