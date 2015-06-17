using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LegoManager
{
    public class App : Application
    {
        public static LegoManagerService LegoService { get; private set; }

        static App()
        {
            LegoService = new LegoManagerService();
        }

        public App()
        {
            // The root page of your application
            var view = new LegoSetListView();
            var navView = new NavigationPage(view);
            MainPage = navView;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
