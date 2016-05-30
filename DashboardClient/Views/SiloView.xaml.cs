using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DashboardClient.Views
{
	public partial class SiloView : ContentView
	{
		public StackOrientation Orientation 
		{
			get { return (StackOrientation)GetValue (OrientationProperty); }
			set { SetValue (OrientationProperty, value); }
		}

		public SiloView ()
		{
			InitializeComponent ();

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
			if (Orientation == StackOrientation.Vertical) 
			{
				Grid.SetColumn (SiloContent, 0);
				Grid.SetColumnSpan (SiloContent, 5);
				Grid.SetRowSpan (SiloContent, 1);

				Grid.SetColumn (VersionContent, 0);
				Grid.SetColumnSpan (VersionContent, 5);
				Grid.SetRowSpan (VersionContent, 1);

				Grid.SetColumn (CPUContent, 0);
				Grid.SetColumnSpan (CPUContent, 5);
				Grid.SetRowSpan (CPUContent, 1);

				Grid.SetColumn (MemoryContent, 0);
				Grid.SetColumnSpan (MemoryContent, 5);
				Grid.SetRowSpan (MemoryContent, 1);

				Grid.SetColumn (GrainContent, 0);
				Grid.SetColumnSpan (GrainContent, 5);
				Grid.SetRowSpan (GrainContent, 1);

				Grid.SetRow (SiloContent, 0);
				Grid.SetRow (VersionContent, 1);
				Grid.SetRow (CPUContent, 2);
				Grid.SetRow (MemoryContent, 3);
				Grid.SetRow (GrainContent, 4);

				//SiloContent.PlaceInGrid    (0, 0, 5, 1);
				//VersionContent.PlaceInGrid (1, 0, 5, 1);
				//CPUContent.PlaceInGrid     (2, 0, 5, 1);
				//MemoryContent.PlaceInGrid  (3, 0, 5, 1);
				//GrainContent.PlaceInGrid   (4, 0, 5, 1);
			} 
			else 
			{
				Grid.SetRow (SiloContent, 0);
				Grid.SetRowSpan (SiloContent, 5);
				Grid.SetColumnSpan (SiloContent, 1);

				Grid.SetRow (VersionContent, 0);
				Grid.SetRowSpan (VersionContent, 5);
				Grid.SetColumnSpan (VersionContent, 1);

				Grid.SetRow (CPUContent, 0);
				Grid.SetRowSpan (CPUContent, 5);
				Grid.SetColumnSpan (CPUContent, 1);

				Grid.SetRow (MemoryContent, 0);
				Grid.SetRowSpan (MemoryContent, 5);
				Grid.SetColumnSpan (MemoryContent, 1);

				Grid.SetRow (GrainContent, 0);
				Grid.SetRowSpan (GrainContent, 5);
				Grid.SetColumnSpan (GrainContent, 1);

				Grid.SetColumn (SiloContent, 0);
				Grid.SetColumn (VersionContent, 1);
				Grid.SetColumn (CPUContent, 2);
				Grid.SetColumn (MemoryContent, 3);
				Grid.SetColumn (GrainContent, 4);

				//SiloContent.PlaceInGrid    (0, 0, 1, 5);
				//VersionContent.PlaceInGrid (0, 1, 1, 5);
				//CPUContent.PlaceInGrid     (0, 2, 1, 5);
				//MemoryContent.PlaceInGrid  (0, 3, 1, 5);
				//GrainContent.PlaceInGrid   (0, 4, 1, 5);
			}
		}
	}
}
