using System;
using System.Threading.Tasks;
using mqttadmin.Models;
using mqttadmin.Services;

namespace mqttadmin.ViewModels
{
    public class EditBrokerItemPageViewModel : BaseViewModel
    {
        // Create and declare our ViewModel class constructor
        public EditBrokerItemPageViewModel(INavigationService navService) : base(navService)
        {
            // If we are creating a new item, we need to update the title
            if (App.SelectedItem == null)
            {
                Title = "Adding Medicine Details";
                App.SelectedItem = new BrokerItem();
                //DateDoseTaken = DateTime.Now;
            }
            else
            {
                Title = "Editing Medicine Details";
            }
        }

        // Checks to see if we have entered in a Brand Name and Description
        public bool Save()
        {
            if (App.SelectedItem != null && !string.IsNullOrEmpty(App.SelectedItem.Host) && !string.IsNullOrEmpty(App.SelectedItem.Port))
            {
                new Database.Database().SaveItem(App.SelectedItem);
            }
            else
            {
                return false;
            }
            return true;
        }

        // Extract all fields entered within the form
        public string Host
        {
            get { return App.SelectedItem.Host; }
            set
            {
                App.SelectedItem.Host = value;
                OnPropertyChanged();
            }
        }

        public string Port
        {
            get { return App.SelectedItem.Port; }
            set { App.SelectedItem.Port = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get { return App.SelectedItem.Username; }
            set { App.SelectedItem.Username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return App.SelectedItem.Password; }
            set { App.SelectedItem.Password = value; OnPropertyChanged(); }
        }

        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
                // Check to see if we are creating a new item
                if (App.SelectedItem == null)
                {
                    Host = "New";
                    Port = "1883";
                }
            });
        }
    }
}

