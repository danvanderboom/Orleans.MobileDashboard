using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DashboardClient
{
	public partial class App : Application
	{
		public App ()
		{
			var bs = new Bootstrapper ();
			bs.Run (this);
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
	}
}

