using System;
using Prism.Unity;
using Xamarin.Forms;

namespace DashboardClient
{
	public class Bootstrapper : UnityBootstrapper
	{
		//protected override void OnInitialized ()
		//{
		//}

		protected override Xamarin.Forms.Page CreateMainPage ()
		{
			//return (Page)Container.Resolve<MasterPage> ();
			return new MasterPage (Container);
		}

		protected override void RegisterTypes ()
		{
			Container.RegisterTypeForNavigation<MasterPage> ();
		}
	}
}
