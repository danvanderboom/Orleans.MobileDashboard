using System;
using System.Collections.Generic;
using DashboardClient.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xamarin.Forms;

namespace DashboardClient
{
	public partial class MasterPage : ContentPage
	{
		IUnityContainer Container;
		IEventAggregator Events;

		// cached views
		View dashboardView = null;
		View clusterMapView = null;
		View historyView = null;
		View settingsView = null;

		double LastWidth, LastHeight;

		public MasterPage (IUnityContainer container)
		{
			Container = container;

			InitializeComponent ();

			SizeChanged += OnSizeChanged;

			Events = Container.Resolve<IEventAggregator> ();

			MenuBarFrame.Content = container.Resolve<MenuBarView> ();
			CurrentViewFrame.Content = container.Resolve<ClusterDashboardView> ();

			Events.GetEvent<NavigateEvent> ().Subscribe (Navigate);
		}

		void Navigate (string target)
		{
			switch (target) {
			case "Dashboard":
				CurrentViewFrame.Content = dashboardView ?? Container.Resolve<ClusterDashboardView> ();
				break;
			case "Cluster Map":
				CurrentViewFrame.Content = clusterMapView ?? Container.Resolve<ClusterMapView> ();
				break;
			//case "History":
			//	MainContentView.Content = historyView ?? Container.Resolve<HistoryView> ();
			//	break;
			//case "Settings":
			//	MainContentView.Content = settingsView ?? Container.Resolve<SettingsView> ();
			//	break;
			}
		}

		void OnSizeChanged (object sender, EventArgs e)
		{
			if (Width == LastWidth && Height == LastHeight)
				return;

			LastWidth = Width;
			LastHeight = Height;

			//if (width > 600)
			//	EnableSplitLayout ();
			//else
			//	EnablePhoneLayout ();

			if (Width > Height)
				SetLandscapeLayout ();
			else
				SetPortraitLayout ();
		}

		void SetPortraitLayout ()
		{
			CurrentViewFrame.PlaceInGrid (0, 0, 1, 2);
			MenuBarFrame.PlaceInGrid (1, 0, 1, 2);

			Events.GetEvent<OrientationEvent> ().Publish ("Portrait");
		}

		void SetLandscapeLayout ()
		{
			CurrentViewFrame.PlaceInGrid (0, 0, 2, 1);
			MenuBarFrame.PlaceInGrid (0, 1, 2, 1);

			Events.GetEvent<OrientationEvent> ().Publish ("Landscape");
		}
	}
}
