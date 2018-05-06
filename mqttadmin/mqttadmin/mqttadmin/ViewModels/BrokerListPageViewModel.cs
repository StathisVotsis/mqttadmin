using System.Collections.ObjectModel;
using System.Threading.Tasks;
using mqttadmin.Services;

namespace mqttadmin.ViewModels
{

    // Declare our MedicineListItem class object
    public class BrokerListItem
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BrokerListPageViewModel : BaseViewModel
    {
        // Create our MedicineList Observable Collection
        public ObservableCollection<BrokerListItem> BrokerList;

        // Create and declare our ViewModel class constructor
        public BrokerListPageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Retrieve the Medicine items from our SQLite database
        public void GetBrokerItems()
        {
            // Specify our List Collection to store the items being read
            BrokerList = new ObservableCollection<BrokerListItem>();

            // Iterate through each item stored within our SQLite database
            foreach (var item in new Database.Database().GetItems())
            {
                // Add each item to our MedicineList Collection
                BrokerList.Add(new BrokerListItem
                {
                    Id = item.Id,
                    Host = item.Host,
                    Port = item.Port,
                    Username = item.Username,
                    Password = item.Password,
                });
            }
        }

        // View Model Initialise method
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}
