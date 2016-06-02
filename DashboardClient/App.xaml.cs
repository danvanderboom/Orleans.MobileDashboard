using Prism.Unity;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DashboardClient
{
	public partial class App : Prism.Unity.PrismApplication
    {
		public App ()
		{
            //var bs = new Bootstrapper();
            //bs.Run(this);

            //this.MainPage = (Page)Container.Resolve(typeof(MasterPage), "MasterPage");

            //MainPage = new MasterPage(Container);
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.Navigate("MainPage");
            NavigationService.NavigateAsync<MasterPage>();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MasterPage>();
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

