using System;
using mqttadmin.Services;
using mqttadmin.ViewModels;
using Xamarin.Forms;


namespace mqttadmin.Pages
{ 
	public partial class BrokerListPage : ContentPage
	{
        // Return the binding context for our ViewModel
        BrokerListPageViewModel _viewModel
        {
            get { return BindingContext as BrokerListPageViewModel; }
        }

        public BrokerListPage()
        {
            InitializeComponent();

            // Initialise our Page Title
            this.Title = "Broker Items Listing";

            // Declare and initialise our Model Binding Context
            this.BindingContext = new BrokerListPageViewModel(DependencyService.Get<INavigationService>());
            BrokerListView.ItemSelected += brokerListView_ItemSelected;
        }

        private Action Add()
        {
            return async () =>
            {
                App.SelectedItem = null;
                await _viewModel.Navigation.NavigateTo<EditBrokerItemPageViewModel>();
            };
        }

        async void brokerListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedItem = new Database.Database().GetItem((e.SelectedItem as BrokerListItem).Id);
            await _viewModel.Navigation.NavigateTo<EditBrokerItemPageViewModel>();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            // Get the selected item to be deleted from our ListView
            var selectedItem = (BrokerListItem)((MenuItem)sender).CommandParameter;

            // Prompt the user with a confirmation dialog to confirm
            var alertResult = await DisplayAlert("Delete Broker Item", "Proceed and delete Broker item?", "OK", "Cancel");
            if (alertResult == true)
            {
                // Remove item from our SQLite Database and MedicineList collection
                var itemDeleted = new Database.Database().DeleteItem(selectedItem.Id);
                _viewModel.BrokerList.Remove(selectedItem);
            }
            else
                return;
        }

        ToolbarItem toolBarItem;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize our View Model
            if (_viewModel != null)
            {
                await _viewModel.Init();
            }

            // Call our GetMedicineItems method to populate our Collection
            _viewModel.GetBrokerItems();

            // Set up and initialise the binding for our ListView
            BrokerListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
            BrokerListView.BindingContext = _viewModel.BrokerList;
            ToolbarItems.Add(toolBarItem = new ToolbarItem("Add", null, Add(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolBarItem);
        }
    }
}