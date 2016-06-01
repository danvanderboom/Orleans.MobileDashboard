using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xamarin.Forms;

namespace DashboardClient.Views
{
	public partial class SiloView : ContentView
	{
		IUnityContainer Container;
		IEventAggregator Events;

		public StackOrientation Orientation 
		{
			get { return (StackOrientation)GetValue (OrientationProperty); }
			set { SetValue (OrientationProperty, value); }
		}

		public SiloView (IUnityContainer container, IEventAggregator events)
		{
			Container = container;
			Events = events;

			InitializeComponent ();

			Events.GetEvent<OrientationEvent> ().Subscribe (
				o => Orientation = (o == ViewOrientation.Portrait) ? StackOrientation.Horizontal : StackOrientation.Vertical);

			UpdateOrientation ();
		}

		public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
			propertyName: "Orientation",
			returnType: typeof (StackOrientation),
			declaringType: typeof (SiloView),
			defaultValue: StackOrientation.Vertical,
			propertyChanged: OrientationPropertyChanged);

		static void OrientationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var silo = bindable as SiloView;
			if (silo == null)
				return;

			if (!(newValue is StackOrientation) || newValue == oldValue)
				return;

			silo.UpdateOrientation ();
		}

		void UpdateOrientation ()
		{
			var columns = 6;

			if (Orientation == StackOrientation.Vertical) 
			{
				SiloContent.SetGridColumn (0, columns);
				SiloContent.SetGridRow (0, 1);

				VersionContent.SetGridColumn (0, columns);
				VersionContent.SetGridRow (1, 1);

				CPUContent.SetGridColumn (0, columns);
				CPUContent.SetGridRow (2, 1);

				MemoryContent.SetGridColumn (0, columns);
				MemoryContent.SetGridRow (3, 1);

				GrainContent.SetGridColumn (0, columns);
				GrainContent.SetGridRow (4, 1);

				ActivationsContent.SetGridColumn (0, columns);
				ActivationsContent.SetGridRow (5, 1);
			} 
			else 
			{
				SiloContent.SetGridColumn (0, 1);
				SiloContent.SetGridRow (0, columns);

				VersionContent.SetGridColumn (1, 1);
				VersionContent.SetGridRow (0, columns);

				CPUContent.SetGridColumn (2, 1);
				CPUContent.SetGridRow (0, columns);

				MemoryContent.SetGridColumn (3, 1);
				MemoryContent.SetGridRow (0, columns);

				GrainContent.SetGridColumn (4, 1);
				GrainContent.SetGridRow (0, columns);

				ActivationsContent.SetGridColumn (5, 1);
				ActivationsContent.SetGridRow (0, columns);
			}
		}
	}
}
