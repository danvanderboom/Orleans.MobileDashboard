using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace DashboardClient.Droid
{
	[Activity (Label = "Orleans Dashboard", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			//TabLayoutResource = Resource.Layout.Tabbar;
			//ToolbarlbarResource = Resource.Layout.Toolbar;

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new DashboardClient.App ());

			// reference themes
			var x = typeof (Xamarin.Forms.Themes.DarkThemeResources);
			x = typeof (Xamarin.Forms.Themes.LightThemeResources);
			x = typeof (Xamarin.Forms.Themes.Android.UnderlineEffect);
		}
	}
}
