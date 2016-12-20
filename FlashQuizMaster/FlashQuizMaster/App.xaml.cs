using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace FlashQuizMaster
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new FlashQuizMaster.MainPage();
        //}
        static DbRepository repository;
        public static DbRepository Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = new DbRepository();
                }
                return repository;
            }
        }

        public App()
        {
            //MainPage = new NavigationPage(new Pages.Test());
            MainPage = new NavigationPage(new HomePage());
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
