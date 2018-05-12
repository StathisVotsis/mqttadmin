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

        public string host;

        public string Host { get { return host; } set { host = value; PropertyChanged(this, new PropertyChangedEventArgs("Host")); } }

        public string port;

        public string Port { get { return port; } set { port = value; PropertyChanged(this, new PropertyChangedEventArgs("Port")); } }

        public string username;

        public string Username { get { return username; } set { username = value; PropertyChanged(this, new PropertyChangedEventArgs("Username")); } }

        public string password;

        public string Password {
            get { return password; }
            set { password = value; PropertyChanged(this, new PropertyChangedEventArgs("Password")); } }

        public ICommand SubmitCommand { get; set; }

        public DBItem item; //initialize a new DBItem

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
            //this.host = App.dbitemController.GetDBItem().Host;
        }

        public void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "LoginAlert", Username);
            }
            string var = App.dbitemController.GetDBItem().Host;
            if(var == null)
            {
                App.dbitemController.SaveDBItem(new DBItem { Id = 0, Host = host, Port = port, UserName = username, PassWord = password});
            }

        }
    }
}
