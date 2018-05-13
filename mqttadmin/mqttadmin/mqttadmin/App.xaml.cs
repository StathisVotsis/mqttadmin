using mqttadmin.Data;
using mqttadmin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mqttadmin
{
	public partial class App : Application
	{

        static DBItemController dbitemcontroller;

		public App ()
		{
			InitializeComponent();

			MainPage = new LoginView();
		}

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

        public static DBItemController dbitemController
        {
            get
            {
                if(dbitemcontroller == null)
                {
                    dbitemcontroller = new DBItemController();
                }

                return dbitemcontroller;
            }
        }
	}
}
