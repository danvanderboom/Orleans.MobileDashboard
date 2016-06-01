using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xamarin.Forms;

namespace DashboardClient.Views
{
	public partial class ClusterDashboardView : ContentView
	{
		IUnityContainer Container;
		IEventAggregator Events;
		List<SiloView> silos;

		public ClusterDashboardView (IUnityContainer container, IEventAggregator events)
		{
			Container = container;
			Events = events;

			InitializeComponent ();

			Events.GetEvent<OrientationEvent> ().Subscribe (OnOrientationUpdated);

			silos = new List<SiloView> ();

			for (int i = 0; i < 12; i++) 
			{
				var silo = container.Resolve<SiloView> ();
				//silo.HeightRequest = 100;
				silo.WidthRequest = 100;

				silos.Add (silo);

				SiloStack.Children.Add (silo);
			}
		}

		void OnOrientationUpdated (ViewOrientation o)
		{
			ColumnHeaderView.IsVisible = (o == ViewOrientation.Portrait);

			SiloScrollView.Orientation = (o == ViewOrientation.Portrait) ? ScrollOrientation.Vertical : ScrollOrientation.Horizontal;
			SiloStack.Orientation = (o == ViewOrientation.Portrait) ? StackOrientation.Vertical : StackOrientation.Horizontal;

			ResourceSidebar.IsVisible = (o == ViewOrientation.Landscape);

			if (o == ViewOrientation.Portrait)
				SiloScrollView.SetGridColumn (0, 2);
			else
				SiloScrollView.SetGridColumn (1, 1);

		}
	}
}
