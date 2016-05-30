using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xamarin.Forms;

namespace DashboardClient.Views
{
	public partial class MenuBarView : ContentView
	{
		public MenuBarViewModel ViewModel;

		public IUnityContainer Container;
		public IEventAggregator Events;

		public StackOrientation Orientation 
		{
			get { return (StackOrientation)GetValue (OrientationProperty); }
			set { SetValue (OrientationProperty, value); }
		}

		public MenuBarView ()
		{
			InitializeComponent ();
		}

		public MenuBarView (MenuBarViewModel viewModel, IUnityContainer container, IEventAggregator events)
		{
			InitializeComponent ();

			Events = events;
			Container = container;

			BindingContext = ViewModel = viewModel;

			Events.GetEvent<OrientationEvent> ().Subscribe (
				o => Orientation = (o == "Portrait") ? StackOrientation.Horizontal : StackOrientation.Vertical);
		}

		public static readonly BindableProperty OrientationProperty = BindableProperty.Create (
			propertyName: "Orientation",
			returnType: typeof (StackOrientation),
			declaringType: typeof (SiloView),
			defaultValue: StackOrientation.Vertical,
			propertyChanged: OrientationPropertyChanged);

		static void OrientationPropertyChanged (BindableObject bindable, object oldValue, object newValue)
		{
			var menubar = bindable as MenuBarView;
			if (menubar == null)
				return;

			if (!(newValue is StackOrientation) || newValue == oldValue)
				return;

			menubar.UpdateOrientation ();
		}

		void UpdateOrientation ()
		{
			if (Orientation == StackOrientation.Vertical) 
			{
				DashboardButton.SetGridColumn (0, 4);
				DashboardButton.SetGridRow (0, 1);

				ClusterMapButton.SetGridColumn (0, 4);
				ClusterMapButton.SetGridRow (1, 1);

				HistoryButton.SetGridColumn (0, 4);
				HistoryButton.SetGridRow (2, 1);

				SettingsButton.SetGridColumn (0, 4);
				SettingsButton.SetGridRow (3, 1);
			} 
			else 
			{
				DashboardButton.SetGridColumn (0, 1);
				DashboardButton.SetGridRow (0, 4);

				ClusterMapButton.SetGridColumn (1, 1);
				ClusterMapButton.SetGridRow (0, 4);

				HistoryButton.SetGridColumn (2, 1);
				HistoryButton.SetGridRow (0, 4);

				SettingsButton.SetGridColumn (3, 1);
				SettingsButton.SetGridRow (0, 4);
			}
		}
	}
}
