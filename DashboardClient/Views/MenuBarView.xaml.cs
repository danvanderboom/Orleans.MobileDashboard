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

			UpdateOrientation ();
		}

		public MenuBarView (MenuBarViewModel viewModel, IUnityContainer container, IEventAggregator events)
		{
			InitializeComponent ();

			Events = events;
			Container = container;

			BindingContext = ViewModel = viewModel;

			Events.GetEvent<OrientationEvent> ().Subscribe (OnOrientationEventReceived);
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

		void OnOrientationEventReceived (ViewOrientation orientation)
		{
			Orientation = (orientation == ViewOrientation.Portrait) ? StackOrientation.Horizontal : StackOrientation.Vertical;

			UpdateOrientation ();
		}

		void UpdateOrientation ()
		{
			if (Orientation == StackOrientation.Vertical) 
			{
				DashboardContent.SetGridColumn (0, 4);
				DashboardContent.SetGridRow (0, 1);

				ClusterMapContent.SetGridColumn (0, 4);
				ClusterMapContent.SetGridRow (1, 1);

				HistoryContent.SetGridColumn (0, 4);
				HistoryContent.SetGridRow (2, 1);

				SettingsContent.SetGridColumn (0, 4);
				SettingsContent.SetGridRow (3, 1);
			} 
			else 
			{
				DashboardContent.SetGridColumn (0, 1);
				DashboardContent.SetGridRow (0, 4);

				ClusterMapContent.SetGridColumn (1, 1);
				ClusterMapContent.SetGridRow (0, 4);

				HistoryContent.SetGridColumn (2, 1);
				HistoryContent.SetGridRow (0, 4);

				SettingsContent.SetGridColumn (3, 1);
				SettingsContent.SetGridRow (0, 4);
			}
		}
	}
}
