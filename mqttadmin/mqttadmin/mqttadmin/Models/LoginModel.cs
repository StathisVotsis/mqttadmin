using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mqttadmin.Models
{
    public class LoginModel : INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string host;

        public string Host { get { return host; } set { host = value; OnPropertyChanged(); } }

        public string port;

        public string Port { get { return port; } set { port = value; OnPropertyChanged(); } }

        public string username;

        public string Username { get { return username; } set { username = value; OnPropertyChanged(); } }

        public string password;

        public string Password {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        //public string Password
        //{
        //get { return password; }
        //set { password = value; PropertyChanged(this, new PropertyChangedEventArgs("Password")); }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand SubmitCommand2 { get; set; }

        public DBItem item; //initialize a new DBItem

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
            SubmitCommand2 = new Command(OnSubmit2);
           
            try
            {
                var e = App.dbitemController.GetDBItem();
                if (e.Host != null)
                {
                    Host = e.Host;
                    Port = e.Port;
                    Username = e.UserName;
                    Password = e.PassWord;
                }
                else
                {

                }
            }
            catch(Exception)
            {

            }
           
            
            
        }

        public void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "LoginAlert", Username);
            }
            
                App.dbitemController.SaveDBItem(new DBItem { Id = 0, Host = Host, Port = Port, UserName = Username, PassWord = Password});

                var e = App.dbitemController.GetDBItem();
                Host = e.Host;
                Port = e.Port;
                Username = e.UserName;
                Password = e.PassWord;



           

        }

        public void OnSubmit2()
        {
            App.dbitemController.DeleteDBItem(12);
            MessagingCenter.Send(this, "LoginAlert", Username);
            var e = App.dbitemController.GetDBItem();
            Host = e.Host;
            Port = e.Port;
            Username = e.UserName;
            Password = e.PassWord;
        }
    }
}
