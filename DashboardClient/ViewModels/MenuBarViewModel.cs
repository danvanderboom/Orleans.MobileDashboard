using System;
using System.Linq.Expressions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace DashboardClient
{
	public class MenuBarViewModel : BindableBase
	{
		private string _CurrentView;

		private IEventAggregator Events { get; set; }

		public DelegateCommand NavigateDashboardCommand { get; set; }

		public DelegateCommand NavigateClusterMapCommand { get; set; }

		public DelegateCommand NavigateHistoryCommand { get; set; }

		public DelegateCommand NavigateSettingsCommand { get; set; }

		public string CurrentView 
		{
			get { return _CurrentView; }
			set { SetProperty (ref _CurrentView, value); }
		}

		public MenuBarViewModel (IEventAggregator events)
		{
			Events = events;

			NavigateDashboardCommand = new DelegateCommand (NavigateHubs, () => CurrentView != "Dashboard")
				.ObservesProperty (() => CurrentView);

			NavigateClusterMapCommand = new DelegateCommand (NavigateApps, () => CurrentView != "Cluster Map")
				.ObservesProperty (() => CurrentView);

			NavigateHistoryCommand = new DelegateCommand (NavigateHistory, () => CurrentView != "History")
				.ObservesProperty (() => CurrentView);

			NavigateSettingsCommand = new DelegateCommand (NavigateSettings, () => CurrentView != "Settings")
				.ObservesProperty (() => CurrentView);
		}

		void NavigateHubs ()
		{
			CurrentView = "Dashboard";
			Events.GetEvent<NavigateEvent> ().Publish (CurrentView);
		}

		void NavigateApps ()
		{
			CurrentView = "Cluster Map";
			Events.GetEvent<NavigateEvent> ().Publish (CurrentView);
		}

		void NavigateHistory ()
		{
			CurrentView = "History";
			Events.GetEvent<NavigateEvent> ().Publish (CurrentView);
		}

		void NavigateSettings ()
		{
			CurrentView = "Settings";
			Events.GetEvent<NavigateEvent> ().Publish (CurrentView);
		}
	}
}
