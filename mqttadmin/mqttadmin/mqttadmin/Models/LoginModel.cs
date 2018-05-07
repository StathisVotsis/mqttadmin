using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mqttadmin.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string username;

        public string Username { get { return username; } set { username = value; PropertyChanged(this, new PropertyChangedEventArgs("Username")); } }

        public string password;

        public string Password {
            get { return password; }
            set { password = value; PropertyChanged(this, new PropertyChangedEventArgs("Password")); } }

        public ICommand SubmitCommand { get; set; }

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "LoginAlert", Username);
            }
        }
    }
}
