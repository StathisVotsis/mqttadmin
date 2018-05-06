using System;
using mqttadmin.Services;
using mqttadmin.ViewModels;
using Xamarin.Forms;

namespace mqttadmin.Pages
{
	
	public partial class EditBrokerItemPage : ContentPage
	{
        // Return the binding context for our ViewModel
        EditBrokerItemPageViewModel _viewModel
        {
            get { return BindingContext as EditBrokerItemPageViewModel; }
        }

        public EditBrokerItemPage()
        {
            InitializeComponent();

            // Declare and initialise our Model Binding Context
            this.BindingContext = new EditBrokerItemPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(Page.TitleProperty, new Binding(EditBrokerItemPageViewModel.TitlePropertyName));
        }

        private Action Save()
        {
            return async () =>
            {
                // Prompt the user with a confirmation dialog to confirm
                var alertResult = await DisplayAlert("Save Broker Item", "Proceed and save changes?", "OK", "Cancel");
                if (alertResult == true)
                {
                    // Attempt to save our medicine item
                    var saveResult = _viewModel.Save();
                    if (!saveResult)
                        // Error Saving - Must have Brand name and description
                        await DisplayAlert("Error", "Host and Port are required.", "OK");
                    else
                        // Navigate back to the Medicine Listing page
                        await _viewModel.Navigation.RemoveViewFromStack();
                }
                else
                {
                    // Navigate back to the Medicine Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
                }
            };
        }

        ToolbarItem toolbarItem;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize our View Model
            if (_viewModel != null)
            {
                await _viewModel.Init();
            }
            ToolbarItems.Add(toolbarItem = new ToolbarItem("Save", null, Save(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolbarItem);
        }
    }
}