using mqttadmin.Models;
using mqttadmin.Pages;
using mqttadmin.Services;
using mqttadmin.ViewModels;
using Xamarin.Forms;

namespace mqttadmin
{
	public partial class App : Application
	{
		public App ()
		{

            InitializeComponent();

            // The root page of our application
            var mainPage = new NavigationPage(new BrokerListPage());

            // Create an instance of our NavigationService service
            var navService = DependencyService.Get<INavigationService>() as NavigationService;

            // Assign the main page to our navigation service
            navService.XFNavigation = mainPage.Navigation;

            // Register each of our View Models on our Navigation Stack
            navService.RegisterViewMapping(typeof(BrokerListPageViewModel), typeof(BrokerListPage));
            navService.RegisterViewMapping(typeof(EditBrokerItemPageViewModel), typeof(EditBrokerItemPage));

            // Set the root page of your application
            MainPage = mainPage;
        }

        // Declare our Medicine Item model that we will use to store
        // our temporary medicine details
        public static BrokerItem SelectedItem { get; set; }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
